' VbCalCon2: Demonstrating how to ceate and use Assemblies
'
Imports System
Imports MyResearch

Namespace VbCalResearch
    Public Class VBCal : Inherits MyResearch.VBLog ' Our Parent (or base) class
    End Class
End Namespace


Module Module1
    ' Here is our test driver
    Sub TestMain()
        Dim program As New VbCalResearch.VBCal
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
        Dim program As New VbCalResearch.VBCal
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
