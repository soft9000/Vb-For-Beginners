' VBNote: R&D Version 3
' Demonstrates the completed solution. Not only does the database load and save
' ipon object creation and destruction, but the addition of the Remove() routine
' allows us to insure that we start each test with an empty database.
'
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

        ' Here is where we store our data
        Protected aryList As New System.Collections.ArrayList(100)

        ' Here is a class within a class - Called a "nested class"
        Public Class SavedEvent
            Public dtScheduled As String
            Public sComment As String
        End Class

        Public Enum Operation
            opAdd
            opDelete
            opList
            opScheduler
            opError
        End Enum

        Protected Function Add(ByVal dtWhen As DateTime, _
           ByVal sComment As String) As Boolean
            Dim seVal As New SavedEvent
            seVal.dtScheduled = dtWhen.ToString
            seVal.sComment = sComment
            aryList.Add(seVal)
            Add = True
        End Function

        Protected Function Delete(ByVal iHuman As Integer) As Boolean
            If (iHuman >= aryList.Count) Then Return False
            aryList.RemoveAt(iHuman)    ' .Remove is something else!
            Delete = True
        End Function

        Protected Overridable Sub List()
            Dim refVal As SavedEvent
            Dim str As String
            Dim i, iHuman As Integer
            While i < aryList.Count ' human usage, again...
                refVal = aryList(i)
                str = i & ".) " _
                & refVal.dtScheduled _
                & " " & refVal.sComment
                Console.WriteLine(str)
                i = i + 1
            End While
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
                Case "@"
                    ' Run the scheduler
                    If (Execute(CInt(cmdArgs(2)) * 1000, cmdArgs(3)) = True) Then
                        Execute = Operation.opScheduler
                        Dim dtNow As DateTime = System.DateTime.Now
                        If (Add(dtNow, cmdArgs(3)) = False) Then Execute = Operation.opError
                    End If

                Case Else
                    Execute = Operation.opError
            End Select
        End Function
    End Class

    Public Class VBLog : Inherits VBNote
        Dim sFileName As String = "C:\VBLog.TXT"
        ' Our constructor - Called for every new object
        Public Sub New()
            LoadFile()
        End Sub
        ' Our destructor - Called when object is destroyed
        Protected Overrides Sub Finalize()
            SaveFile()
        End Sub

        Public Sub Empty()
            aryList.Clear()
        End Sub

        Private Function LoadFile()
            Dim str As String
            Try
                Dim freader As New System.IO.StreamReader(sFileName)
                Dim seVal As New SavedEvent
                Do
                    str = freader.ReadLine()
                    If str Is Nothing Then
                        Exit Do
                    End If
                    seVal.dtScheduled = str
                    str = freader.ReadLine()
                    If str Is Nothing Then
                        Exit Do
                    End If
                    seVal.sComment = str
                    aryList.Add(seVal)
                Loop Until str Is Nothing
                freader.Close()
            Catch ex As Exception
                Return False
            End Try
            LoadFile = True
        End Function
        Private Function SaveFile()
            Try
                Dim refVal As SavedEvent
                Dim fwriter As New System.IO.StreamWriter(sFileName)
                Dim i As Integer
                ' Convert from "human" form
                For i = 0 To aryList.Count - 1
                    refVal = aryList(i)
                    fwriter.WriteLine(refVal.dtScheduled)
                    fwriter.WriteLine(refVal.sComment)
                Next
                fwriter.Close()
            Catch ex As Exception
                Return False
            End Try
            SaveFile = True
        End Function

    End Class
End Namespace

Module Module1
    ' Here is our test driver
    Sub TestMain()
        Dim program As New MyResearch.VBLog
        Dim cmdArgs(3) As String

        ' Make sure that we start out with an empty database
        program.Empty()

        ' Test Case One: Add a few strings
        cmdArgs(0) = "TestDriver"
        cmdArgs(1) = "+"
        cmdArgs(2) = "Today is the first day"
        If (program.Execute(cmdArgs) <> MyResearch.VBNote.Operation.opAdd) Then
            Console.WriteLine("Error 101.")
            Return
        End If
        cmdArgs(2) = "of the rest of"
        If (program.Execute(cmdArgs) <> MyResearch.VBNote.Operation.opAdd) Then
            Console.WriteLine("Error 102.")
            Return
        End If
        cmdArgs(2) = "my program's life!"
        If (program.Execute(cmdArgs) <> MyResearch.VBNote.Operation.opAdd) Then
            Console.WriteLine("Error 103.")
            Return
        End If
        ' Test Case Two: Test the list operation:
        Dim cmdList(1) As String
        If (program.Execute(cmdList) <> MyResearch.VBNote.Operation.opList) Then
            Console.WriteLine("Error 201.")
            Return
        End If
        ' Test Case Three: Test the delete operation
        cmdArgs(1) = "-"
        cmdArgs(2) = "0"
        If (program.Execute(cmdArgs) <> MyResearch.VBNote.Operation.opDelete) Then
            Console.WriteLine("Error 302.")
            Return
        End If
        If (program.Execute(cmdArgs) <> MyResearch.VBNote.Operation.opDelete) Then
            Console.WriteLine("Error 303.")
            Return
        End If
        If (program.Execute(cmdArgs) <> MyResearch.VBNote.Operation.opDelete) Then
            Console.WriteLine("Error 304.")
            Return
        End If
        ' Test Case Four: Test the error delete operation
        If (program.Execute(cmdArgs) <> MyResearch.VBNote.Operation.opError) Then
            Console.WriteLine("Error 401.")
            Return
        End If
        ' Test Case Five: Test the Scheduler operation
        cmdArgs(1) = "@"
        cmdArgs(2) = "12"
        cmdArgs(3) = "This is a scheduler message"
        If (program.Execute(cmdArgs) = MyResearch.VBNote.Operation.opError) Then
            Console.WriteLine("Error 501.")
            Return
        End If
        Console.WriteLine("Success: All basic test cases pass.")
    End Sub

    ' Here is our driver
    Sub RealMain()
        Dim program As New MyResearch.VBLog
        If (program.Execute(Environment.GetCommandLineArgs()) = MyResearch.VBNote.Operation.opError) Then
            Console.WriteLine("Error.")
        Else
            Console.WriteLine("Success.")
        End If
    End Sub

    Sub Main()
        ' By changing the boolean value to true or false,
        ' we can easily change the program version the we run.
        Dim bTest As Boolean = True
        If (bTest = True) Then
            TestMain()
        Else
            RealMain()
        End If
    End Sub
End Module
