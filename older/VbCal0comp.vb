' VBCal: R&D Version 0
Imports System
Imports System.DateTime ' Assembly: Mscorlib (in Mscorlib.dll)
Imports System.Collections.SortedList


Module Module1
    Class VBNote
        Public Sub New()    ' Our constructor - Called for every new object
            LoadFile()
        End Sub
        Protected Overrides Sub Finalize()    ' Our destructor - Called when object is destroyed
            SaveFile()
        End Sub
        ' Returns -1 on error, else computer number (n-1) of record added
        Public Function Add(ByVal dtWhen As DateTime, ByVal sComment As String) As Integer
            Dim seVal As New SavedEvent
            seVal.dtScheduled = dtWhen.ToString
            seVal.sComment = sComment
            aryList.Add(aryList.Count, seVal)
            Add = aryList.Count - 1
        End Function
        Public Function Delete(ByVal iHuman As Integer) As Boolean
            aryList.RemoveAt(iHuman)    ' .Remove is something else!
            Delete = True
        End Function
        Public Overridable Sub List()
            Dim refVal As SavedEvent
            Dim str As String
            Dim i, iHuman As Integer
            While i < aryList.Count ' human usage, again...
                refVal = aryList.GetByIndex(i)
                str = i & ".) " & refVal.dtScheduled & " " & refVal.sComment
                Console.WriteLine(str)
                i = i + 1
            End While
        End Sub

        ' Here is a class within a class - Called a "nested class"
        Public Class SavedEvent
            Public dtScheduled As String
            Public sComment As String
        End Class

        Protected aryList As New System.Collections.SortedList(100)

        Private Function LoadFile()
            Dim str As String
            Try
                Dim freader As New System.IO.StreamReader("C:\VBNote.TXT")
                Do
                    Dim seVal As New SavedEvent ' new instance each time!
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
                    aryList.Add(aryList.Count, seVal)
                Loop Until str Is Nothing   ' not needed, but a good usage example
                freader.Close()
            Catch ex As Exception
                Return False
            End Try
            LoadFile = True
        End Function
        Private Function SaveFile()
            Try
                Dim refVal As SavedEvent
                Dim fwriter As New System.IO.StreamWriter("C:\VBNote.TXT")
                Dim i As Integer
                For i = 0 To aryList.Count - 1 ' Convert from "human" form
                    refVal = aryList.GetByIndex(i)
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


    Class VBCal ' Re-use via composition
        Private Dim comp as new VBNote()

        Public Sub New()
        End Sub
        Protected Overrides Sub Finalize()
        End Sub
        Public Function Add(ByVal dtWhen As DateTime, ByVal sComment As String) As Integer
            comp.Add(dtWhen, sComment)
        End Function
        Public Function Delete(ByVal iHuman As Integer) As Boolean
            Delete = comp.Delete(iHuman)
        End Function
        Public Sub List()
            comp.List()
        End Sub
    End Class


    Sub Main()
        Dim program As New VBCal
        Dim cmdArgs As String() = Environment.GetCommandLineArgs()
        ' Check to see if we have the right number of parameters
        If cmdArgs.Length < 2 Then
            program.List()
            Exit Sub
        End If

        Dim sRemainder As String

        Select Case cmdArgs(1) ' Evaluate user feature request
            Case "+"
                ' Add a note
                Dim ss As Integer
                Dim str As String
                For ss = 2 To cmdArgs.GetUpperBound(0)
                    sRemainder = sRemainder & " " & cmdArgs(ss)
                Next
                Dim dtNow As DateTime = System.DateTime.Now
                program.Add(dtNow, sRemainder)
            Case "-"
                ' Remove a note (number comes from listing)
                Console.WriteLine("(-) Deleting existing item ...")
                program.Delete(CInt(cmdArgs(2)))
            Case Else
                ' The default is to tell them how to use this tool
                sRemainder = "Usage: " & cmdArgs(0) & " [+ note] [- n] [?]"
                Console.Error.WriteLine(sRemainder)
        End Select
        Console.WriteLine(" [done] ")
    End Sub

End Module
