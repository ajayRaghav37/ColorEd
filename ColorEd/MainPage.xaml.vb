' The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

''' <summary>
''' A basic page that provides characteristics common to most applications.
''' </summary>
Public NotInheritable Class MainPage
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
        Me.Frame.Navigate(GetType(Conventions))
    End Sub

    Private Sub MainPage_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        chkMaze.IsOn = chkMazeIsOn
        BackPan.Background = New SolidColorBrush(PanBack)
    End Sub
End Class
