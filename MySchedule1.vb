' NOTE: You might need to use a reference to system.dll to get this to compile!
' Example: vbc /r:system.dll MySchedule1.vb
'
Imports System
Imports System.Timers

Module Module1

    Public Class MySchedule

        Public Sub RunNow()
            ' Create a multi-second Timer
            Dim aTimer As New System.Timers.Timer(3000)
            ' Give the event something to do every time ...
            AddHandler aTimer.Elapsed, AddressOf OnTimedEvent
            ' Let the event re-sechedule itself
            aTimer.AutoReset = True
            ' Start it running!
            aTimer.Enabled = True

            Dim str As String
            ' Wait for the timer event until the user presses 'q' ...
            Console.WriteLine("Press 'q' to quit.")
            str = Console.ReadLine()
            While str <> "q"
                str = Console.ReadLine()
            End While
        End Sub

        ' Do something for each timed interval
        Private Sub OnTimedEvent(ByVal s As Object, _
                      ByVal e As ElapsedEventArgs)
            Console.WriteLine("   > Time!")
        End Sub
    End Class

    Public Sub Main()
        Dim prog As New MySchedule
        prog.RunNow()
    End Sub

End Module ' MySchedule1.vb
