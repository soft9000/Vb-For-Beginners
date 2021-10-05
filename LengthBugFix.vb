Imports System
Module Module1

    Sub Main()
        Dim cmdArgs As String() = Environment.GetCommandLineArgs()
        Dim i As Integer
        For i = 1 To cmdArgs.Length ' Value is n+1 (human count)
            ' Array wants n-1 (computer count), so we subtract 1
            Console.WriteLine(cmdArgs(i - 1))
        Next
    End Sub
End Module ' LengthBugFix.vb