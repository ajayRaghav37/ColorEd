' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class LoadingMe
    Inherits Page

    ''' <summary>
    ''' Invoked when this page is about to be displayed in a Frame.
    ''' </summary>
    ''' <param name="e">Event data that describes how this page was reached.  The Parameter
    ''' property is typically used to configure the page.</param>
    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)

    End Sub

    Dim WithEvents tmrLoad As DispatcherTimer = New DispatcherTimer
    Dim WheelSpeed As Integer
    Dim RotAngle As Integer
    Dim iterats As Integer
    Dim Loadback As Windows.UI.Color = Windows.UI.Color.FromArgb(255, 210, 72, 39)
    Dim D_R As Integer
    Dim D_G As Integer
    Dim D_B As Integer
    Dim hMount As Double
    Dim TempR As Double
    Dim TempG As Double
    Dim TempB As Double

    Private Sub LoadingMe_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'The wheel will be increase rotation from 0 to 1 second -d
        'The wheel will begin to fade out after 0.7 seconds and will be invisible after 1 second -d
        'The background will go a little dark at 0.3 seconds and it will go to panback after 1 second -d
        'The mountain will rise from bottom after 1 second -d
        'Every other static object will fade in from 0.5 to 1 second

        'The timer will do all actions in splits of 40 occurences
        'PanBack.R = 210
        'PanBack.G = 72
        'PanBack.B = 39
        'hMount = imgMount.ActualHeight
        'imgMount.Margin = New Thickness(0, 0, 0, 0)
        'testAll.Text = hMount
        tmrLoad.Interval = New TimeSpan(0, 0, 0, 0, 1)
        tmrLoad.Start()
        D_R = PanBack.R - 90
        D_G = PanBack.G - 24
        D_B = PanBack.B - 15
    End Sub

    Private Sub tmrLoad_Tick(sender As Object, e As Object) Handles tmrLoad.Tick
        'testAll.Text = Loadback.R & " " & Loadback.G & " " & Loadback.B
        AddAngle(WheelSpeed)
        WheelSpeed += 1
        iterats += 1
        imgMount.Margin = New Thickness(0, 0, 0, hMount * (iterats - 40) / 40)
        If iterats <= 12 Then
            Loadback.R -= 10
            Loadback.G -= 4
            Loadback.B -= 2
            BackPan.Background = New SolidColorBrush(Loadback)
        Else
            TempR = 90 + (iterats - 10) * D_R / 28
            TempG = 24 + (iterats - 10) * D_G / 28
            TempB = 15 + (iterats - 10) * D_B / 28
            FixByte(TempR)
            FixByte(TempG)
            FixByte(TempB)
            Loadback = Windows.UI.Color.FromArgb(255, TempR, TempG, TempB)
            BackPan.Background = New SolidColorBrush(Loadback)
        End If
        If iterats >= 33 Then
            grdLoad.Opacity -= 1 / 8
        End If
        If iterats >= 20 Then
            TopRow.Opacity += 0.05
            hyperlinkButton.Opacity = TopRow.Opacity
            hyperlinkButton1.Opacity = TopRow.Opacity
            hyperlinkButton2.Opacity = TopRow.Opacity
            hyperlinkButton3.Opacity = TopRow.Opacity
            hyperlinkButton4.Opacity = TopRow.Opacity
            btnNextPage.Opacity = TopRow.Opacity
            If iterats <= 28 Then
                'grdLoad.Margin = New Thickness(0, 0, 0, grdLoad.Margin.Bottom + 8)
                '9x8 = 72
                '16 in 20
                '14 in 21
                '12 in 22
                '10 in 23
                '8 in 24
                '6 in 25
                '4 in 26
                '2 in 27
                '0 in 28
                grdLoad.Margin = New Thickness(0, 0, 0, grdLoad.Margin.Bottom + ((28 - iterats) * 2))
            Else
                'grdLoad.Margin = New Thickness(0, 0, 0, grdLoad.Margin.Bottom - 40)
                '12x40=480
                '2 in 29
                '16 in 36
                '18 in 37
                '20 in 38
                '22 in 29
                '24 in 40
                'multiply all by three
                grdLoad.Margin = New Thickness(0, 0, 0, grdLoad.Margin.Bottom - (6 * (iterats - 28)))
            End If
        End If
        If iterats = 40 Then
            tmrLoad.Stop()
            Me.Frame.Navigate(GetType(MainPage))
        End If
    End Sub

    Private Sub AddAngle(AddThat As Double)
        Dim AngleBuffer As Double
        Dim Rotator As New RotateTransform
        AngleBuffer = RotAngle + AddThat
        If AngleBuffer > 180 Then
            Rotator.Angle = AngleBuffer - 360
        ElseIf AngleBuffer < -180 Then
            Rotator.Angle = AngleBuffer + 360
        Else
            Rotator.Angle = AngleBuffer
        End If
        imgWheel.RenderTransform = Rotator
        RotAngle = Rotator.Angle
    End Sub

    Private Sub imgMount_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles imgMount.SizeChanged
        hMount = e.NewSize.Height
        'testAll.Text = hMount
    End Sub

    Private Sub LoadingMe_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        chkMaze.IsOn = chkMazeIsOn
    End Sub
End Class
