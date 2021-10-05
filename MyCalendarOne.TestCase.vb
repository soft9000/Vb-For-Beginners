Imports System
Imports System.DateTime ' Assembly: Mscorlib (in Mscorlib.dll)
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Collections.ArrayList


Module Module1
    Class MyCalendar
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
            aryList.Add(seVal)
            Add = aryList.Count - 1
        End Function
        Public Function Delete(ByVal iHuman As Integer) As Boolean
            aryList.RemoveAt(iHuman)    ' .Remove is something else!
            Delete = True
        End Function
        Public Sub List()
            Dim refVal As SavedEvent
            Dim str As String
            Dim i, iHuman As Integer
            While i < aryList.Count ' human usage, again...
                refVal = aryList(i)
                str = i & ".) " & refVal.dtScheduled & " " & refVal.sComment
                Console.WriteLine(str)
                i = i + 1
            End While
        End Sub

        ' Here is a class withing a class - Called a "nested class"
        Public Class SavedEvent
            Public dtScheduled As String
            Public sComment As String
        End Class

        Protected aryList As New System.Collections.ArrayList(100)

        Private Function LoadFile()
            Dim str As String
            Try
                Dim freader As New System.IO.StreamReader("C:\MyCalendar.TXT")
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
                    aryList.Add(seVal)
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
                Dim fwriter As New System.IO.StreamWriter("C:\MyCalendar.TXT")
                Dim i As Integer
                For i = 0 To aryList.Count - 1 ' Convert from "human" form
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

    Sub Main()
        Dim program As New MyCalendar
        Dim dtNow As New DateTime
        dtNow = System.DateTime.Now
        program.Add(dtNow, "One")
        Dim toDelete As Integer = program.Add(dtNow, "Two")
        program.Add(dtNow, "Three")
        program.List()
        program.Delete(toDelete)
        Console.WriteLine("=====")
        program.List()
    End Sub

End Module
