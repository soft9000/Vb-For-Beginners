' VBCalCon: R&D Version 4
' Adding some color!
Imports System
Imports System.DateTime ' Assembly: Mscorlib (in Mscorlib.dll)
Imports System.Collections.SortedList

Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices


' Change this reference to FreeTools2 - Don't forget to
' rename the namespace in FreeTools2 as well, or it will 
' not work!
Imports FreeTools2

Module Module1

    Class VBNote
        Private con As New FreeTools2.ConColor.ConColor7

        Public Sub New()    ' Our constructor - Called for every new object
            con.Cls(FreeTools2.ConColor.ConColor7.Foreground.Blue, FreeTools2.ConColor.ConColor7.Background.Green)
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


    Class VBCal             ' Be sure to put the line break in here!
        Inherits VBNote     ' Our Parent (or base) class
        Public Sub New()
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        Public Overrides Sub List()     ' This class will provide a new list style
            Dim refVal As SavedEvent
            Dim str As String
            Dim i, iHuman As Integer
            Dim dtBest As DateTime
            While i < aryList.Count ' human usage, again...
                refVal = aryList.GetByIndex(i)
                dtBest = refVal.dtScheduled
                str = i & ".) " & Format("D", dtBest) & " " & refVal.sComment
                Console.WriteLine(str)
                i = i + 1
            End While
        End Sub
    End Class


    Sub Main()

        ' Update this to use FreeTools2
        Dim MyClock As Clock
        ' Why do we *not* need to use `new` here?
        Dim MyTime As Clock.SystemTime = Clock.CurrentTime()

        Select Case MyTime.wDayOfWeek
            Case 0
                Console.Write("Sunday ")
            Case 1
                Console.Write("Monday ")
            Case 2
                Console.Write("Tuesday ")
            Case 3
                Console.Write("Wednesday ")
            Case 4
                Console.Write("Thursday ")
            Case 5
                Console.Write("Friday ")
            Case 6
                Console.Write("Saturday ")
            Case Else
                Console.Write("? ")
        End Select

        Console.WriteLine("{0}/{1}/{2}", MyTime.wYear, MyTime.wMonth, MyTime.wDay)

        ' Do not remove this, unless you have a better driver to use!
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
