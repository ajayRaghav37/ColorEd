Imports Windows.UI.Notifications
Imports Windows.Data.Xml.Dom

Module Universal
    Public ProgSet = Windows.Storage.ApplicationData.Current.RoamingSettings
    Public ColCol() As String = {"Aquamarine", "Black", "Blue", "Brown", "Cyan", "Green", "Gray", "Indigo", "Lime", "Maroon", "Magenta", "Navy", "Olive", "Orange", "Pink", "Purple", "Red", "Silver", "Teal", "Turquoise", "Violet", "Yellow", "White", "Alice Blue", "Antique White", "Aqua", "Azure", "Beige", "Bisque", "Blanched Almond", "Blue Violet", "Burly Wood", "Cadet Blue", "Chartreuse", "Chocolate", "Coral", "Cornflower Blue", "Cornsilk", "Crimson", "Dark Blue", "Dark Cyan", "Dark Goldenrod", "Dark Gray", "Dark Green", "Dark Khaki", "Dark Magenta", "Dark Olive Green", "Dark Orange", "Dark Orchid", "Dark Red", "Dark Salmon", "Dark Sea Green", "Dark Slate Blue", "Dark Slate Gray", "Dark Turquoise", "Dark Violet", "Deep Pink", "Deep Sky Blue", "Dim Gray", "Dodger Blue", "Firebrick", "Floral White", "Forest Green", "Fuchsia", "Gainsboro", "Ghost White", "Gold", "Goldenrod", "Green Yellow", "Honeydew", "Hot Pink", "Indian Red", "Ivory", "Khaki", "Lavender", "Lavender Blush", "Lawn Green", "Lemon Chiffon", "Light Blue", "Light Coral", "Light Cyan", "Light Goldenrod Yellow", "Light Gray", "Light Green", "Light Pink", "Light Salmon", "Light Sea Green", "Light Sky Blue", "Light Slate Gray", "Light Steel Blue", "Light Yellow", "Lime Green", "Linen", "Medium Aquamarine", "Medium Blue", "Medium Orchid", "Medium Purple", "Medium Sea Green", "Medium Slate Blue", "Medium Spring Green", "Medium Turquoise", "Medium Violet Red", "Midnight Blue", "Mint Cream", "Misty Rose", "Moccasin", "Navajo White", "Old Lace", "Olive Drab", "Orange Red", "Orchid", "Pale Goldenrod", "Pale Green", "Pale Turquoise", "Pale Violet Red", "Papaya Whip", "Peach Puff", "Peru", "Plum", "Powder Blue", "Rosy Brown", "Royal Blue", "Saddle Brown", "Salmon", "Sandy Brown", "Sea Green", "Sea Shell", "Sienna", "Sky Blue", "Slate Blue", "Slate Gray", "Snow", "Spring Green", "Steel Blue", "Tan", "Thistle", "Tomato", "Wheat", "White Smoke", "Yellow Green"}
    Public Randomizer As New Random
    Public chkMazeIsOn As Boolean = (GtSt("MazeOn", 0) = 1)
    Public S_r As Integer = GtSt("P_R", 210)
    Public S_g As Integer = GtSt("P_G", 72)
    Public S_b As Integer = GtSt("P_B", 39)
    Public WithEvents ColorMaze As DispatcherTimer = New DispatcherTimer()
    Public PanBack As Windows.UI.Color = Windows.UI.Color.FromArgb(255, S_r, S_g, S_b)
    
    Public Function MaxOf(Num1 As Double, Num2 As Double, Num3 As Double) As Double
        If Num1 >= Num2 And Num1 >= Num3 Then
            Return Num1
        Else
            If Num2 >= Num3 Then
                Return Num2
            Else
                Return Num3
            End If
        End If
    End Function

    Public Function MinOf(Num1 As Double, Num2 As Double, Num3 As Double) As Double
        If Num1 <= Num2 And Num1 <= Num3 Then
            Return Num1
        Else
            If Num2 <= Num3 Then
                Return Num2
            Else
                Return Num3
            End If
        End If
    End Function

    Public Function FromName(ColName As String) As Windows.UI.Color
        Dim [property] = System.Reflection.RuntimeReflectionExtensions.GetRuntimeProperty(GetType(Windows.UI.Colors), ColName.Replace(" ", ""))
        Return DirectCast([property].GetValue(Nothing), Windows.UI.Color)
    End Function

    Public Function RGB(vRed As Byte, vGreen As Byte, vBlue As Byte, Optional vAlpha As Byte = 255) As SolidColorBrush
        RGB = New SolidColorBrush(Windows.UI.Color.FromArgb(vAlpha, vRed, vGreen, vBlue))
    End Function

    Public Sub RGBtoHLS(r As Double, g As Double, b As Double, ByRef h As Double, ByRef s As Double, ByRef l As Double)
        r /= 255
        g /= 255
        b /= 255
        Dim v As Double
        Dim m As Double
        Dim vm As Double
        Dim r2 As Double, g2 As Double, b2 As Double
        h = 0
        ' default to black
        s = 0
        l = 0
        v = Math.Max(r, g)
        v = Math.Max(v, b)
        m = Math.Min(r, g)
        m = Math.Min(m, b)
        l = (m + v) / 2.0
        If l <= 0.0 Then
            h *= 360
            If h < 0 Then
                h += 360
            ElseIf h >= 360 Then
                h -= 360
            End If
            s = s * 100
            l = l * 100
            Return
        End If
        vm = v - m
        s = vm
        If s > 0.0 Then
            s /= If((l <= 0.5), (v + m), (2.0 - v - m))
        Else
            h *= 360
            If h < 0 Then
                h += 360
            ElseIf h >= 360 Then
                h -= 360
            End If
            s = s * 100
            l = l * 100
            Return
        End If
        r2 = (v - r) / vm
        g2 = (v - g) / vm
        b2 = (v - b) / vm
        If r = v Then
            h = (If(g = m, 5.0 + b2, 1.0 - g2))
        ElseIf g = v Then
            h = (If(b = m, 1.0 + r2, 3.0 - b2))
        Else
            h = (If(r = m, 3.0 + g2, 5.0 - r2))
        End If
        h /= 6
        h *= 360
        If h < 0 Then
            h += 360
        ElseIf h >= 360 Then
            h -= 360
        End If
        s = s * 100
        l = l * 100
    End Sub
    Public Sub RGBtoCMYK(r As Integer, g As Integer, b As Integer, ByRef c As Single, ByRef m As Single, ByRef y As Single, ByRef k As Single)
        Dim R_ As Single
        Dim G_ As Single
        Dim B_ As Single
        R_ = r / 255
        G_ = g / 255
        B_ = b / 255
        k = 1 - MaxOf(R_, G_, B_)
        If k = 1 Then
            c = 0
            m = 0
            y = 0
        Else
            c = (1 - R_ - k) / (1 - k)
            m = (1 - G_ - k) / (1 - k)
            y = (1 - B_ - k) / (1 - k)
            c = c * 100
            m = m * 100
            y = y * 100
            k = k * 100
        End If
    End Sub

    Public Sub RGBtoHSB(r As Integer, g As Integer, b As Integer, ByRef h As Single, ByRef s As Single, ByRef v As Single)
        Dim min__1 As Integer, max__2 As Integer, delta As Integer
        min__1 = MinOf(r, g, b)
        max__2 = MaxOf(r, g, b)
        v = max__2 * 100 / 255
        ' v
        delta = max__2 - min__1
        If max__2 <> 0 And delta <> 0 Then
            s = 100 * delta / max__2
            If r = max__2 Then
                h = (g - b) / delta
                ' between yellow & magenta
            ElseIf g = max__2 Then
                h = 2 + (b - r) / delta
            Else
                ' between cyan & yellow
                h = 4 + (r - g) / delta
            End If
            ' between magenta & cyan
        Else
            ' s
            ' r = g = b = 0		// s = 0, v is undefined
            s = 0
            h = 0
        End If
        h *= 60
        ' degrees
        If h < 0 Then
            h += 360
        End If
    End Sub

    Public Sub HSBtoRGB(ByRef r As Single, ByRef g As Single, ByRef b As Single, h As Single, s As Single, v As Single)
        Dim i As Integer
        Dim f As Single, p As Single, q As Single, t As Single
        'h=0 to 360
        s /= 100
        v /= 100
        If s = 0 Then
            ' achromatic (grey)
            r = v
            b = v
            g = v
        Else
            h /= 60
            ' sector 0 to 5
            i = Math.Floor(h)
            f = h - i
            ' factorial part of h
            p = v * (1 - s)
            q = v * (1 - s * f)
            t = v * (1 - s * (1 - f))
            Select Case i
                Case 0
                    r = v
                    g = t
                    b = p
                Case 1
                    r = q
                    g = v
                    b = p
                Case 2
                    r = p
                    g = v
                    b = t
                Case 3
                    r = p
                    g = q
                    b = v
                Case 4
                    r = t
                    g = p
                    b = v
                Case Else
                    ' case 5:
                    r = v
                    g = p
                    b = q
            End Select
        End If
        r *= 255
        g *= 255
        b *= 255
    End Sub

    Public Sub HLStoRGB(ByRef r As Single, ByRef g As Single, ByRef b As Single, h As Single, l As Single, s As Single)
        l /= 100
        s /= 100
        Dim c As Single = (1 - Math.Abs(2 * l - 1)) * s
        Dim x As Single = c * (1 - Math.Abs(((h / 60) Mod 2) - 1))
        Dim m As Single = l - (c / 2)
        If (h < 60) Then
            r = c
            g = x
            b = 0
        ElseIf (h < 120) Then
            r = x
            g = c
            b = 0
        ElseIf (h < 180) Then
            r = 0
            g = c
            b = x
        ElseIf (h < 240) Then
            r = 0
            g = x
            b = c
        ElseIf (h < 300) Then
            r = x
            g = 0
            b = c
        Else
            r = c
            g = 0
            b = x
        End If
        r = (r + m) * 255
        g = (g + m) * 255
        b = (b + m) * 255
    End Sub

    Public Sub CMYKtoRGB(ByRef r As Single, ByRef g As Single, ByRef b As Single, c As Single, m As Single, y As Single, k As Single)
        c /= 100
        m /= 100
        y /= 100
        k /= 100
        r = 255 * (1 - c) * (1 - k)
        g = 255 * (1 - m) * (1 - k)
        b = 255 * (1 - y) * (1 - k)
    End Sub

    Public i As Integer
    Public D_r As Integer
    Public D_g As Integer
    Public D_b As Integer
    Public r_D As Integer
    Public g_D As Integer
    Public b_D As Integer

    Private Sub ColorMaze_Tick(sender As Object, e As Object) Handles ColorMaze.Tick
        If i = 0 Then
            D_r = Randomizer.Next(256)
            D_g = Randomizer.Next(256)
            D_b = Randomizer.Next(256)
            r_D = D_r - S_r
            g_D = D_g - S_g
            b_D = D_b - S_b
        End If
        i += 1
        PanBack.R = S_r + (i * r_D / 20)
        PanBack.G = S_g + (i * g_D / 20)
        PanBack.B = S_b + (i * b_D / 20)
        SvSt("P_R", PanBack.R)
        SvSt("P_G", PanBack.G)
        SvSt("P_B", PanBack.B)
        If i = 20 Then
            i = 0
            S_r = D_r
            S_g = D_g
            S_b = D_b
        End If
    End Sub

    Public Sub CreateNewTile(nID As Integer)
        TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(True)
        Dim tileXml As XmlDocument = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideSmallImageAndText04)
        Dim ColName As String = ColCol(Randomizer.Next(ColCol.Count))
        Dim tiletextattributes As XmlNodeList = tileXml.GetElementsByTagName("text")
        tiletextattributes(0).InnerText = "ColorEd"
        tiletextattributes(1).InnerText = ColName & vbNewLine & "#" & FromName(ColName).ToString.Substring(3)
        Dim tileimageattributes As XmlNodeList = tileXml.GetElementsByTagName("image")
        Dim imageelement As XmlElement = DirectCast(tileimageattributes(0), XmlElement)
        imageelement.SetAttribute("src", "ms-appx:///Assets/Colors/" & ColName & ".png")
        imageelement.SetAttribute("alt", tiletextattributes(1).InnerText)

        Dim squaretile As XmlDocument = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText02)
        Dim tiletextsquare As XmlNodeList = squaretile.GetElementsByTagName("text")
        tiletextsquare(0).InnerText = "ColorEd"
        tiletextsquare(1).InnerText = ColName & vbNewLine & "#" & FromName(ColName).ToString.Substring(3)

        Dim node As IXmlNode = tileXml.ImportNode(squaretile.GetElementsByTagName("binding").Item(0), True)
        tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node)

        Dim mytilenot As TileNotification = New TileNotification(tileXml)
        mytilenot.Tag = nID.ToString
        TileUpdateManager.CreateTileUpdaterForApplication.Update(mytilenot)
    End Sub

    Public Sub RefreshTiles()
        CreateNewTile(1)
        CreateNewTile(2)
        CreateNewTile(3)
        CreateNewTile(4)
        CreateNewTile(5)
    End Sub

    Public Function GtSt(SettingName As String, Optional DefaultVal As Double = 0) As Double
        If Not ProgSet.Values.ContainsKey(SettingName) Then
            SvSt(SettingName, DefaultVal)
        End If
        GtSt = ProgSet.Values(SettingName)
    End Function

    Public Sub SvSt(SettingName As String, SettingValue As Double)
        ProgSet.Values(SettingName) = SettingValue
    End Sub

    Public Sub FixByte(ByRef ToFix As Double)
        If ToFix < 0 Then
            ToFix = 0
        ElseIf ToFix > 255 Then
            ToFix = 255
        End If
    End Sub
End Module
