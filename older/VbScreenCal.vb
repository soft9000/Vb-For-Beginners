Imports System
Imports FreeTools2
Imports VbCalFinal

Module Module1

    Class MyForm
        Public clrBack As FreeTools2.ConColor.ConColor7.Background
        Protected sDate As String = "Date: "
        Protected sComment As String = "Comment: "
        Public Sub Show(ByVal con As FreeTools2.ConColor.ConColor7)
            ' Green text on white background
            Dim bg As FreeTools2.ConColor.ConColor7.Background = _
            FreeTools2.ConColor.ConColor7.Background.Green + _
            FreeTools2.ConColor.ConColor7.Background.Blue + _
            FreeTools2.ConColor.ConColor7.Background.Red + _
            FreeTools2.ConColor.ConColor7.Background.Intensity
            Dim fg As FreeTools2.ConColor.ConColor7.Foreground = _
            FreeTools2.ConColor.ConColor7.Foreground.Green
            con.Cls(bg, fg)
            con.Put(1, 1, "VbCal User Interface")
            con.Put(1, 2, "--------------------")
            con.TextColor(FreeTools2.ConColor.ConColor7.Foreground.Blue + bg)
            con.Put(5, 5, sDate + "MM/DD/YY")
            con.Put(5, 6, sComment)
            clrBack = bg
        End Sub
        Public Function GetDate(ByVal con As FreeTools2.ConColor.ConColor7) As DateTime
            con.TextColor(FreeTools2.ConColor.ConColor7.Foreground.Red + clrBack)
            con.GotoXY(5 + sDate.Length, 5)
            Dim sResult As String = Console.ReadLine()
            Try
                GetDate = sResult
            Catch
                ' gigo - Feel free to change this operation if you want to.
                ' ----
                ' As it is, the result will be the default value of 0, or
                ' 12:00 AM, when an error is encountered
                '
                ' Here is a way to use the present date and time here rather 
                ' than to report an error:
                ' GetDate = Now()
            End Try
        End Function
        Public Function GetComment(ByVal con As FreeTools2.ConColor.ConColor7) As String
            con.TextColor(FreeTools2.ConColor.ConColor7.Foreground.Red + clrBack)
            con.GotoXY(5 + sComment.Length, 6)
            Dim sResult As String = Console.ReadLine()
            GetComment = sResult
        End Function
    End Class

    Sub Main()
        Dim con As New FreeTools2.ConColor.ConColor7
        Dim screen As New MyForm
        screen.Show(con)

        Dim Cal As New VbCalFinal.VBCal
        ' Here is a fancy way to call several functions at once:
        ' (Not a good idea here because it allows users to continue on an error)
        If (Cal.Add(screen.GetDate(con), screen.GetComment(con)) = -1) Then
            Console.Error.WriteLine("Error: Item not added!")
        Else
            Console.WriteLine()
            Console.WriteLine("New item added")
            Console.WriteLine()
            Cal.List()
        End If
    End Sub

End Module
