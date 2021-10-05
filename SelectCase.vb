Imports System
Module Module1
    Sub Main()
        Dim sInput As String
        sInput = Console.ReadLine()
        Dim iTest As Integer = CInt(sInput)
        Select Case iTest ' Evaluate use input.
            Case 1
                Console.WriteLine("We're number 1")
            Case 2 To 10  ' (inclusive)
                Console.WriteLine("Between 2 and 10 (inclusive)")
            Case 11
            Case 12
                Console.Write("Either 11 or 12")
                Console.WriteLine(".. ok..")
            Case Else
                Console.WriteLine("More than a dozen...")
        End Select
    End Sub
End Module ' SelectCase.vb