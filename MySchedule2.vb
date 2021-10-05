' NOTE: You might need to use a reference to system.dll to get this to compile!
' Example: vbc /r:system.dll MySchedule2.vb
'
Imports System
Imports System.Timers

Module Module1

    Public Class MySchedule
        Private iCount As Integer
        Public Sub Execute()
            ' Create a multi-second Timer
            Dim aTimer As New System.Timers.Timer(1000)
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

        ' Give the timer something to do every time the event is ready...
        Private Sub OnTimedEvent(ByVal s As Object, ByVal e As ElapsedEventArgs)
            iCount = iCount + 1
            Dim str As String = " > " & s.GetType.FullName & " has run " & iCount & " times."
            Console.WriteLine(str)
        End Sub
    End Class

    Public Sub Main()
        Dim MyProgram As New MySchedule
        MyProgram.Execute()
    End Sub

End Module ' MySchedule2.vb
