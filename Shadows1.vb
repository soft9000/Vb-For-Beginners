' Demonstrate the implication of using Shadows.
'
Imports System

Module Module1
    Class MyCalc
        Protected myData As Integer = 1
        Function Calc() As Integer
            Return myData
        End Function
    End Class
    Class MyCalc2 : Inherits MyCalc
        Protected Shadows myData As String = "MyCalc2"
        Shadows Function Calc() As String
            Return myData
        End Function
        Public Sub Forced()
            Console.WriteLine(MyBase.myData)
        End Sub
    End Class

    Sub Main()
        Dim cmdArgs() As String
        Dim parent As New MyCalc
        Dim shadow As New MyCalc2
        Console.WriteLine(parent.Calc())
        shadow.Forced()
        Console.WriteLine(shadow.Calc())
    End Sub
End Module ' Shadow1.vb

