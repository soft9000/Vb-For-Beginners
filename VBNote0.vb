' VBNote: R&D Version 0
Imports System
Imports System.Timers
Imports Microsoft.VisualBasic

Namespace MyResearch
    Public Class MySchedule
        Private sMessage As String
        Private aTimer As System.Timers.Timer

        ' Updated to allow us to "pass in" a specified number 
        ' of seconds to wait ...
        Public Overloads Function Execute(ByVal iSeconds As Integer, _
           ByVal sMsg As String) As Boolean
            If (sMsg Is Nothing) Then Return False
            If (iSeconds = Nothing) Then Return False
            sMessage = sMsg ' How we save a value for use by a delegate
            aTimer = New System.Timers.Timer(iSeconds)
            aTimer.AutoReset = True
            aTimer.Start()
            AddHandler aTimer.Elapsed, AddressOf OnTimedEvent
            While aTimer.Enabled = True
                ' do nothing...
            End While
            Return True
        End Function

        ' Allow the timer to show our message when it is time
        Private Sub OnTimedEvent(ByVal s As Object, _
           ByVal e As ElapsedEventArgs)
            MsgBox(sMessage)
            aTimer.Stop()
        End Sub
    End Class

    Public Class VBNote : Inherits MySchedule

        Public Enum Operation
            opAdd
            opDelete
            opList
            opError
        End Enum

        Protected Function Add(ByVal dtWhen As DateTime, _
           ByVal sComment As String) As Boolean
            Add = False
        End Function

        Protected Function Delete(ByVal iHuman As Integer) As Boolean
            Delete = False
        End Function

        Protected Overridable Sub List()
        End Sub

        ' This is just about the same as used for our calculator
        Public Overloads Function Execute(ByVal cmdArgs As String()) _
           As Operation
            If cmdArgs.Length <= 2 Then
                List()
                Return Operation.opList
            End If

            ' Set our default return value
            Execute = Operation.opError

            Select Case cmdArgs(1) ' Evaluate user feature request
                Case "+"
                    ' Add a note
                    Dim dtNow As DateTime = System.DateTime.Now
                    If (Add(dtNow, cmdArgs(2)) = True) Then
                        Execute = Operation.opAdd
                    End If
                Case "-"
                    ' Remove a note (number comes from listing)
                    If (Delete(CInt(cmdArgs(2))) = True) Then
                        Execute = Operation.opDelete
                    End If

                Case Else
                    Execute = Operation.opError
            End Select
        End Function
    End Class
End Namespace

Module Module1
    Sub Main()
    End Sub
End Module
