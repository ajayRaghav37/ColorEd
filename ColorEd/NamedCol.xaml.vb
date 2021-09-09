' The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

''' <summary>
''' A basic page that provides characteristics common to most applications.
''' </summary>
Public NotInheritable Class NamedCol
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
    Dim OldCol As Windows.UI.Color
    Dim IsFading As Boolean
    Dim InQueue As Boolean
    Dim Q_r As Integer
    Dim Q_g As Integer
    Dim Q_b As Integer

    Private Sub ColList_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ColList.SelectionChanged
        If ColList.SelectedIndex = 0 Then
            ColList.SelectedIndex = 1
        ElseIf ColList.SelectedIndex = 24 Then
            ColList.SelectedIndex = 25
        End If
        SvSt("NCsel", ColList.SelectedIndex)
        'Dim TestURI As New Uri("ms-appx:///Assets/Colors/" & ColList.Items.Item(ColList.SelectedIndex) & ".png")
        'Dim TestBmp As New BitmapImage(TestURI)
        'TestCols.Source = TestBmp

        'Dim DatPack As New DataTransfer.DataPackage
        'DatPack.SetText(ColList.Items.Item(ColList.SelectedIndex))
        'DataTransfer.Clipboard.SetContent(DatPack)

        Dim NewCol As Windows.UI.Color = FromName(ColList.Items.Item(ColList.SelectedIndex))
        If chkFade.IsOn Then
            FadeCol(OldCol.R, OldCol.G, OldCol.B, NewCol.R, NewCol.G, NewCol.B)
        Else
            FillRtg.Fill = New SolidColorBrush(NewCol)
        End If
        html_C.Text = "#" & NewCol.ToString.Substring(3)
        rgb_R.Text = NewCol.R.ToString
        rgb_G.Text = NewCol.G.ToString
        rgb_B.Text = NewCol.B.ToString
        OldCol.R = NewCol.R
        OldCol.G = NewCol.G
        OldCol.B = NewCol.B

        'RGB to HSB
        Dim d1 As Single, d2 As Single, d3 As Single, d4 As Single
        RGBtoHSB(NewCol.R, NewCol.G, NewCol.B, d1, d2, d3)
        hsb_H.Text = Math.Round(d1)
        hsb_S.Text = Math.Round(d2)
        hsb_B.Text = Math.Round(d3)
        'RGB To HLS
        RGBtoHLS(NewCol.R, NewCol.G, NewCol.B, d1, d2, d3)
        hls_H.Text = Math.Round(d1)
        hls_L.Text = Math.Round(d3)
        hls_S.Text = Math.Round(d2)
        RGBtoCMYK(NewCol.R, NewCol.G, NewCol.B, d1, d2, d3, d4)
        cmyk_C.Text = Math.Round(d1)
        cmyk_M.Text = Math.Round(d2)
        cmyk_Y.Text = Math.Round(d3)
        cmyk_K.Text = Math.Round(d4)
    End Sub

    Private Async Sub NamedCol_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If GtSt("FadeOn", 1) = 0 Then
            chkFade.IsOn = False
        End If
        Await Task.Delay(50)
        ColorMazeU.Interval = New TimeSpan(0, 0, 0, 0, 50)
        If chkMazeIsOn Then
            chkMaze.IsOn = True
            ColorMazeU.Start()
        End If
        OldCol.R = 255
        OldCol.G = 87
        OldCol.B = 47
        FillList()
        ColList.SelectedIndex = GtSt("NCsel", 1)
        If ColList.SelectedIndex < ColList.Items.Count - 5 Then
            ColList.ScrollIntoView(ColList.Items.Item(ColList.SelectedIndex + 4))
        Else
            ColList.ScrollIntoView(ColList.Items.Item(ColList.Items.Count - 1))
        End If
    End Sub

    Private Sub FillList()
        ColList.Items.Add("--- Basic Colors ---")
        For TempNum As Integer = 0 To 22
            ColList.Items.Add(ColCol(TempNum))
        Next
        ColList.Items.Add("--- Complex Colors ---")
        For TempNum As Integer = 23 To ColCol.Count - 1
            ColList.Items.Add(ColCol(TempNum))
        Next
    End Sub

    Public Async Sub FadeCol(S_r As Integer, S_g As Integer, S_b As Integer, D_r As Integer, D_g As Integer, D_b As Integer)
        If IsFading Then
            InQueue = True
            Q_r = D_r
            Q_g = D_g
            Q_b = D_b
            Exit Sub
        End If
        IsFading = True
        Dim r_D As Integer = D_r - S_r
        Dim g_D As Integer = D_g - S_g
        Dim b_D As Integer = D_b - S_b
        'Fade will be in 10 parts and 1 second
        For i As Integer = 1 To 10
            Await Task.Delay(50)
            FillRtg.Fill = RGB(S_r + (i * r_D / 10), S_g + (i * g_D / 10), S_b + (i * b_D / 10))
        Next
        IsFading = False
        If InQueue Then
            FadeCol(D_r, D_g, D_b, Q_r, Q_g, Q_b)
            InQueue = False
        End If
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
        Me.Frame.Navigate(GetType(ColExer))
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As RoutedEventArgs) Handles btnPrevPage.Click
        Me.Frame.Navigate(GetType(Conventions))
    End Sub

    Private Sub chkFade_Toggled(sender As Object, e As RoutedEventArgs) Handles chkFade.Toggled
        If chkFade.IsOn Then
            SvSt("FadeOn", 1)
        Else
            SvSt("FadeOn", 0)
        End If
    End Sub

    Private Sub NamedCol_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        chkMaze.IsOn = chkMazeIsOn
        BackPan.Background = New SolidColorBrush(PanBack)
    End Sub
End Class
