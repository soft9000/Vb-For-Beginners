' Here is an example of a Forwardly Modal Calculator.
' Feel free to update this code to include other operations as you see fit.
'
Imports System
Module Module1
    Function Calc(ByVal cmdArgs() As String) As Boolean
        Dim iTotal, ss As Integer ' Notice that we dimension 2 variable here
        Dim bAdding As Boolean
        Dim sInput As String
        bAdding = True ' The default is to add
        ss = 1         ' Skip the program name
        Do While ss < cmdArgs.Length
            sInput = cmdArgs(ss)
            Select Case sInput ' Evaluate user input.
                Case "+"
                    bAdding = True
                Case "-"
                    bAdding = False
                Case Else
                    If (bAdding = True) Then
                        iTotal = iTotal + CInt(sInput)
                    Else
                        iTotal = iTotal - CInt(sInput)
                    End If
            End Select
            ss = ss + 1
        Loop
        Console.Write(" > The result is ")
        Console.WriteLine(iTotal)
        Return True
    End Function
    Sub Main()
        Dim cmdArgs As String() = Environment.GetCommandLineArgs()
        Try
            If (Calc(cmdArgs) = False) Then
                Console.WriteLine("Error!")
            End If
        Catch
            Console.WriteLine("Invalid Input!")
        End Try
    End Sub
End Module ' fmcalc2.vb
