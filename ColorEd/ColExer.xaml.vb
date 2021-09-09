' The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

''' <summary>
''' A basic page that provides characteristics common to most applications.
''' </summary>
Public NotInheritable Class ColExer
    Inherits Common.LayoutAwarePage

    ''' <summary>
    ''' Populates the page with content passed during navigation.  Any saved state is also
    ''' provided when recreating a page from a prior session.
    ''' </summary>
    ''' <param name="navigationParameter">The parameter value passed to
    ''' <see cref="Frame.Navigate"/> when this page was initially requested.
    ''' </param>
    ''' <param name="pageState">A dictionary of state preserved by this page during an earlier
    ''' session.  This will be null the first time a page is visited.</param>
    Protected Overrides Sub LoadState(navigationParameter As Object, pageState As Dictionary(Of String, Object))

    End Sub

    ''' <summary>
    ''' Preserves state associated with this page in case the application is suspended or the
    ''' page is discarded from the navigation cache.  Values must conform to the serialization
    ''' requirements of <see cref="Common.SuspensionManager.SessionState"/>.
    ''' </summary>
    ''' <param name="pageState">An empty dictionary to be populated with serializable state.</param>
    Protected Overrides Sub SaveState(pageState As Dictionary(Of String, Object))

    End Sub

    Dim NewR As Byte = GtSt("C_R", 255)
    Dim NewG As Byte = GtSt("C_G", 87)
    Dim NewB As Byte = GtSt("C_B", 47)
    Dim TempR As Byte = GtSt("C_R", 255)
    Dim TempG As Byte = GtSt("C_G", 87)
    Dim TempB As Byte = GtSt("C_B", 47)
    Dim CurCol As Windows.UI.Color = Windows.UI.Color.FromArgb(255, GtSt("C_R", 255), GtSt("C_G", 87), GtSt("C_B", 47))
    Dim TgtColC As Windows.UI.Color

    Private Sub RGB_R_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles RGB_R.ValueChanged
        NewR = RGB_R.Value
        R_RGB.Text = Math.Round(RGB_R.Value)
        CheckExCol()
    End Sub

    Private Sub RGB_G_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles RGB_G.ValueChanged
        NewG = RGB_G.Value
        G_RGB.Text = Math.Round(RGB_G.Value)
        CheckExCol()
    End Sub

    Private Sub RGB_B_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles RGB_B.ValueChanged
        NewB = RGB_B.Value
        B_RGB.Text = Math.Round(RGB_B.Value)
        CheckExCol()
    End Sub

    Private Sub HSB_H_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles HSB_H.ValueChanged
        HSBtoRGB(NewR, NewG, NewB, HSB_H.Value, HSB_S.Value, HSB_B.Value)
        H_HSB.Text = Math.Round(HSB_H.Value)
        CheckExCol()
        HSBtoRGB(TempR, TempG, TempB, HSB_H.Value, 100, 100)
        SatChange.Color = Windows.UI.Color.FromArgb(255, TempR, TempG, TempB)
    End Sub

    Private Sub HSB_S_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles HSB_S.ValueChanged
        HSBtoRGB(NewR, NewG, NewB, HSB_H.Value, HSB_S.Value, HSB_B.Value)
        S_HSB.Text = Math.Round(HSB_S.Value)
        CheckExCol()
    End Sub

    Private Sub HSB_B_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles HSB_B.ValueChanged
        HSBtoRGB(NewR, NewG, NewB, HSB_H.Value, HSB_S.Value, HSB_B.Value)
        B_HSB.Text = Math.Round(HSB_B.Value)
        CheckExCol()
    End Sub

    Private Sub HLS_H_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles HLS_H.ValueChanged
        HLStoRGB(NewR, NewG, NewB, HLS_H.Value, HLS_L.Value, HLS_S.Value)
        H_HLS.Text = Math.Round(HLS_H.Value)
        CheckExCol()
        HLStoRGB(TempR, TempG, TempB, HLS_H.Value, 50, 100)
        Sat_HLS.Color = Windows.UI.Color.FromArgb(255, TempR, TempG, TempB)
        Lig_HLS.Color = Sat_HLS.Color
    End Sub

    Private Sub HLS_S_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles HLS_S.ValueChanged
        HLStoRGB(NewR, NewG, NewB, HLS_H.Value, HLS_L.Value, HLS_S.Value)
        S_HLS.Text = Math.Round(HLS_S.Value)
        CheckExCol()
    End Sub

    Private Sub HLS_L_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles HLS_L.ValueChanged
        HLStoRGB(NewR, NewG, NewB, HLS_H.Value, HLS_L.Value, HLS_S.Value)
        L_HLS.Text = Math.Round(HLS_L.Value)
        CheckExCol()
    End Sub

    Private Sub CMYK_C_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles CMYK_C.ValueChanged
        CMYKtoRGB(NewR, NewG, NewB, CMYK_C.Value, CMYK_M.Value, CMYK_Y.Value, CMYK_K.Value)
        C_CMYK.Text = Math.Round(CMYK_C.Value)
        CheckExCol()
    End Sub

    Private Sub CMYK_M_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles CMYK_M.ValueChanged
        CMYKtoRGB(NewR, NewG, NewB, CMYK_C.Value, CMYK_M.Value, CMYK_Y.Value, CMYK_K.Value)
        M_CMYK.Text = Math.Round(CMYK_M.Value)
        CheckExCol()
    End Sub

    Private Sub CMYK_Y_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles CMYK_Y.ValueChanged
        CMYKtoRGB(NewR, NewG, NewB, CMYK_C.Value, CMYK_M.Value, CMYK_Y.Value, CMYK_K.Value)
        Y_CMYK.Text = Math.Round(CMYK_Y.Value)
        CheckExCol()
    End Sub

    Private Sub CMYK_K_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles CMYK_K.ValueChanged
        CMYKtoRGB(NewR, NewG, NewB, CMYK_C.Value, CMYK_M.Value, CMYK_Y.Value, CMYK_K.Value)
        K_CMYK.Text = Math.Round(CMYK_K.Value)
        CheckExCol()
    End Sub

    Private Sub SetNewTarget()
        Dim MyColSel As Integer
        MyColSel = Randomizer.Next(139)
        TgtColC = FromName(ColCol(MyColSel))
        TgtCol.Fill = New SolidColorBrush(TgtColC)
        TgtColNm.Text = ColCol(MyColSel)
        SvSt("Tgt_C", MyColSel)
    End Sub

    Private Sub CheckExCol()
        MyColNm.Text = "(Unnamed)"
        CurCol = Windows.UI.Color.FromArgb(255, NewR, NewG, NewB)
        SvSt("C_R", NewR)
        SvSt("C_G", NewG)
        SvSt("C_B", NewB)
        MyCol.Fill = New SolidColorBrush(CurCol)
        For TempNum As Integer = 0 To ColCol.Count - 1
            If CurCol = FromName(ColCol(TempNum)) Then
                MyColNm.Text = ColCol(TempNum)
                Exit For
            End If
        Next
        If TgtColC = CurCol Then
            If WellDone.Text = "Well done! Try this one..." Then
                WellDone.Text = "Great! Wanna try again?"
            Else
                WellDone.Text = "Well done! Try this one..."
            End If
            WellDone.Visibility = Windows.UI.Xaml.Visibility.Visible
            SetNewTarget()
        End If
    End Sub

    Private Sub colRGB_Click(sender As Object, e As RoutedEventArgs) Handles colRGB.Click
        panRGB.Visibility = Windows.UI.Xaml.Visibility.Visible
        panHSB.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panHLS.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panCMYK.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        RGB_R.Value = CurCol.R
        RGB_G.Value = CurCol.G
        RGB_B.Value = CurCol.B
        colRGB.IsEnabled = False
        colHSB.IsEnabled = True
        colHLS.IsEnabled = True
        colCMYK.IsEnabled = True
        SvSt("Envir", 0)
    End Sub

    Private Sub colHSB_Click(sender As Object, e As RoutedEventArgs) Handles colHSB.Click
        panRGB.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panHSB.Visibility = Windows.UI.Xaml.Visibility.Visible
        panHLS.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panCMYK.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        RGBtoHSB(CurCol.R, CurCol.G, CurCol.B, HSB_H.Value, HSB_S.Value, HSB_B.Value)
        colRGB.IsEnabled = True
        colHSB.IsEnabled = False
        colHLS.IsEnabled = True
        colCMYK.IsEnabled = True
        SvSt("Envir", 1)
    End Sub

    Private Sub colHLS_Click(sender As Object, e As RoutedEventArgs) Handles colHLS.Click
        panRGB.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panHSB.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panHLS.Visibility = Windows.UI.Xaml.Visibility.Visible
        panCMYK.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        RGBtoHLS(CurCol.R, CurCol.G, CurCol.B, HLS_H.Value, HLS_S.Value, HLS_L.Value)
        colRGB.IsEnabled = True
        colHSB.IsEnabled = True
        colHLS.IsEnabled = False
        colCMYK.IsEnabled = True
        SvSt("Envir", 2)
    End Sub

    Private Sub colCMYK_Click(sender As Object, e As RoutedEventArgs) Handles colCMYK.Click
        panRGB.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panHSB.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panHLS.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        panCMYK.Visibility = Windows.UI.Xaml.Visibility.Visible
        RGBtoCMYK(CurCol.R, CurCol.G, CurCol.B, CMYK_C.Value, CMYK_M.Value, CMYK_Y.Value, CMYK_K.Value)
        colRGB.IsEnabled = True
        colHSB.IsEnabled = True
        colHLS.IsEnabled = True
        colCMYK.IsEnabled = False
        SvSt("Envir", 3)
    End Sub

    Private Sub hyperlinkButton3_Click(sender As Object, e As RoutedEventArgs) Handles hyperlinkButton3.Click
        Me.Frame.Navigate(GetType(Conventions))
    End Sub

    Private Sub hyperlinkButton4_Click(sender As Object, e As RoutedEventArgs) Handles hyperlinkButton4.Click
        Me.Frame.Navigate(GetType(NamedCol))
    End Sub

    Private Sub hyperlinkButton_Click(sender As Object, e As RoutedEventArgs) Handles hyperlinkButton.Click
        Me.Frame.Navigate(GetType(ColExer))
    End Sub

    Private Sub hyperlinkButton1_Click(sender As Object, e As RoutedEventArgs) Handles hyperlinkButton1.Click
        Me.Frame.Navigate(GetType(ColBlind))
    End Sub

    Private Sub hyperlinkButton2_Click(sender As Object, e As RoutedEventArgs) Handles hyperlinkButton2.Click
        Me.Frame.Navigate(GetType(MainPage))
    End Sub

    Dim WithEvents ColorMazeU As DispatcherTimer = New DispatcherTimer()

    Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ColorMazeU.Interval = New TimeSpan(0, 0, 0, 0, 50)
        If chkMazeIsOn Then
            chkMaze.IsOn = True
            ColorMazeU.Start()
        End If
    End Sub

    Private Sub chkMaze_Toggled(sender As Object, e As RoutedEventArgs) Handles chkMaze.Toggled
        chkMazeIsOn = chkMaze.IsOn
        If chkMazeIsOn Then
            SvSt("MazeOn", 1)
            ColorMaze.Start()
            ColorMazeU.Start()
        Else
            SvSt("MazeOn", 0)
            ColorMaze.Stop()
            ColorMazeU.Stop()
        End If
    End Sub

    Private Sub ColorMazeU_Tick(sender As Object, e As Object) Handles ColorMazeU.Tick
        BackPan.Background = New SolidColorBrush(PanBack)
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As RoutedEventArgs) Handles btnNextPage.Click
        Me.Frame.Navigate(GetType(ColBlind))
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As RoutedEventArgs) Handles btnPrevPage.Click
        Me.Frame.Navigate(GetType(NamedCol))
    End Sub

    Private Sub ColExer_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        chkMaze.IsOn = chkMazeIsOn
        BackPan.Background = New SolidColorBrush(PanBack)
        RGB_R.Value = GtSt("C_R", 255)
        RGB_G.Value = GtSt("C_G", 87)
        RGB_B.Value = GtSt("C_B", 47)
        If GtSt("Tgt_C", 139) < 139 Then
            TgtColC = FromName(ColCol(GtSt("Tgt_C")))
            TgtCol.Fill = New SolidColorBrush(TgtColC)
            TgtColNm.Text = ColCol(GtSt("Tgt_C"))
        Else
            SetNewTarget()
        End If
        Select Case GtSt("Envir", 0)
            Case 1
                colHSB_Click(sender, e)
            Case 2
                colHLS_Click(sender, e)
            Case 3
                colCMYK_Click(sender, e)
        End Select
    End Sub
End Class
