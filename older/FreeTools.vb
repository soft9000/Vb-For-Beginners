' FreeTools: R&D Version 0.1
' DLL USAGE MEANS THAT THIS CODE WILL ONLY WORK ON WIN32
' PLEASE VIST SOFT9000.COM FOR HELP OR MORE INFORMATION
' 
Imports System
Imports System.Runtime.InteropServices

Namespace FreeTools

    Public Class Handy
        Protected Declare Auto Sub GetSystemDirectory Lib "Kernel32" (ByVal szBuf As StringBuilder, ByVal usize As Integer)
        Protected Declare Auto Sub GetWindowsDirectory Lib "Kernel32" (ByVal szBuf As StringBuilder, ByVal usize As Integer)
        Public Const MAX_PATH As Integer = 256
        ' Here is an easy way to play a build-in sound. See MessageBeep in the Platform SDK for more informaiton
        Public Declare Auto Sub MessageBeep Lib "User32" (ByVal N As Integer)
        ' This function returns the directory where the main system is
        Public Shared Function GetSystemDirectory(ByRef sResult As String) As Boolean
            Dim strB As New StringBuilder(MAX_PATH)
            Try
                GetWindowsDirectory(strB, MAX_PATH)
                sResult = strB.ToString
            Catch
                sResult = "Error!"
                Return False
            End Try
            Return True
        End Function
        ' This function returns the directory where many "built-in" programs live
        Public Shared Function GetSystem32Directory(ByRef sResult As String) As Boolean
            Dim strB As New StringBuilder(MAX_PATH)
            Try
                GetSystemDirectory(strB, MAX_PATH)
                sResult = strB.ToString
            Catch
                sResult = "Error!"
                Return False
            End Try
            Return True
        End Function

    Public Module ConColor
        ' This ConColor7 Class demonstrates how to add some color and
        ' cursor addressability to your WIN32 console applications.
        ' ====
        ' THIS DEMONSTRATES RELATIVELY ADVANCED VB.NET PROGRAMMING!
        ' DO NOT WORRY IF YOU DO NOT COMPLETELY UNDERSAND THIS CODE!
        Public Class ConColor7
            Private hConsole As IntPtr
            Private ConsoleOutputLocation As COORD
            Private ConsoleInfo As CONSOLE_SCREEN_BUFFER_INFO
            Private OriginalColors As Integer
            Public pwAttribute As Integer

            Private Const STD_OUTPUT_HANDLE As Integer = -11
            ' Here is another way to define that ...
            ' Private Const STD_OUTPUT_HANDLE As Integer = &HFFFFFFF5
            Private Const STD_INPUT_HANDLE As Integer = -10
            Private Const STD_ERROR_HANDLE As Integer = -12
            Private Const INVALID_HANDLE_VALUE As Integer = -1

            'Example: Class1.Foreground.Red + Class1.Foreground.Blue + Class1.Foreground.Intensity
            Public Enum Foreground
                Blue = &H1
                Green = &H2
                Red = &H4
                Intensity = &H8
            End Enum

            Public Enum Background
                Blue = &H10
                Green = &H20
                Red = &H40
                Intensity = &H80
            End Enum

            <StructLayout(LayoutKind.Sequential)> _
            Private Structure COORD
                Dim X As Short
                Dim Y As Short
            End Structure

            <StructLayout(LayoutKind.Sequential)> _
            Private Structure SMALL_RECT
                Dim Left As Short
                Dim Top As Short
                Dim Right As Short
                Dim Bottom As Short
            End Structure

            <StructLayout(LayoutKind.Sequential)> _
            Private Structure CONSOLE_SCREEN_BUFFER_INFO
                Dim dwSize As COORD
                Dim dwCursorPosition As COORD
                Dim wAttributes As Integer
                Dim srWindow As SMALL_RECT
                Dim dwMaximumWindowSize As COORD
            End Structure

            Private Declare Auto Function _
                GetStdHandle Lib "kernel32.dll" (ByVal nStdHandle As Integer) As IntPtr

            Private Declare Auto Function _
                GetConsoleScreenBufferInfo Lib "kernel32.dll" (ByVal hConsoleOutput As IntPtr, _
                    ByRef lpConsoleScreenBufferInfo As CONSOLE_SCREEN_BUFFER_INFO) As Integer

            Private Declare Auto Function _
                SetConsoleTextAttribute Lib "kernel32" (ByVal hConsoleOutput As IntPtr, _
                    ByVal wAttributes As Integer) As Long

            ' Here is another way to define that ...
            ' Private Declare Auto Function _
            '       SetConsoleTextAttribute Lib "kernel32.dll" (ByVal consoleOutput As IntPtr, _
            '       ByVal Attributes As UInt16) As Boolean

            Private Declare Auto Function _
                FillConsoleOutputAttribute Lib "kernel32" (ByVal hConsoleOutput As IntPtr, _
                    ByVal wAttributes As Integer, _
                    ByVal nLength As Integer, _
                    ByVal dwWriteCoord As COORD, _
                    ByRef lpNumberOfAttrsWritten As Integer) As Long

            Private Declare Auto Function _
                WriteConsole Lib "kernel32" (ByVal hConsoleOutput As IntPtr, _
                    ByVal lpBuffer As String, _
                    ByVal nNumberOfCharsToWrite As Integer, _
                    ByRef lpNumberOfCharsWritten As Integer, _
                    ByRef lpReserved As Integer) As Boolean

            Private Declare Auto Function _
                SetConsoleCursorPosition Lib "kernel32" (ByVal hConsoleOutput As IntPtr, _
                    ByVal dwWriteCoord As COORD) As Boolean

            Private Declare Auto Function _
                WriteConsoleOutputAttribute Lib "kernel32" (ByVal hConsoleOutput As IntPtr, _
                    ByVal lpAttribute As Integer, _
                    ByRef nLength As Integer, _
                    ByVal dwWriteCoord As COORD, _
                    ByRef lpNumberOfAttrsWritten As Integer) As Boolean

            ' Uck! Too much work...
            Private Declare Auto Function _
                    WriteConsoleOutputCharacter Lib "kernel32.dll" Alias "WriteConsoleOutputCharacterA" (ByVal hConsoleOutput As IntPtr, _
                    ByVal lpCharacter As String, _
                    ByVal nLength As Long, _
                    ByVal dwWriteCoord As COORD, _
                    ByVal lpNumberOfCharsWritten As Long) As Long

            Private Declare Auto Function _
                GetLastError Lib "kernel32" () As Integer

            Sub New()
                hConsole = GetStdHandle(STD_OUTPUT_HANDLE)
                GetConsoleScreenBufferInfo(hConsole, ConsoleInfo)
                OriginalColors = ConsoleInfo.wAttributes
            End Sub

            Public Sub TextColor(ByVal Colors As Integer)
                ' Set the text colors... Converts the enums to ints
                SetConsoleTextAttribute(hConsole, Colors)
            End Sub

            Public Sub ResetColor()
                ' Restore the original colors as stored when instance was created.
                SetConsoleTextAttribute(hConsole, OriginalColors)
            End Sub

            Public Function Cls(ByVal f As Foreground, ByVal b As Background) As Boolean
                Dim cCharsWritten As Integer
                Dim length As Integer = ConsoleInfo.dwSize.X * ConsoleInfo.dwSize.Y
                Dim coord As COORD
                pwAttribute = f + b
                If (FillConsoleOutputAttribute(hConsole, pwAttribute, length, coord, cCharsWritten) = False) Then
                    Return False
                End If
                If (SetConsoleTextAttribute(hConsole, pwAttribute) = False) Then
                    Return False
                End If
                Cls = True
            End Function

            Public Function GotoXY(ByVal x As Integer, ByVal y As Integer) As Boolean
                Dim coord As COORD
                coord.X = x
                coord.Y = y
                If (SetConsoleCursorPosition(hConsole, coord) = False) Then
                    Return False
                End If
                GotoXY = True
            End Function

            Public Function Put(ByVal x As Integer, ByVal y As Integer, ByVal str As String) As Boolean
                If (GotoXY(x, y) = False) Then
                    Return False
                End If
                Dim cbOut, lbReserved As Integer
                If (MyClass.WriteConsole(hConsole, str, str.Length, cbOut, lbReserved) = False) Then
                    Return False
                End If
                Put = True
            End Function

            Public Sub GetLastError(ByRef sResult As String)
                Dim iError As Integer = GetLastError()
                sResult = iError.ToString
            End Sub

        End Class


    End Module
End Namespace


Module Module7
    Public Sub Main()
        Dim Moof As New FreeTools.ConColor7
        Moof.Cls(FreeTools.ConColor7.Foreground.Blue, FreeTools.ConColor7.Background.Green)
        If (Moof.Put(5, 5, "Coo!") = False) Then
            Dim str As String
            Moof.GetLastError(str)
            Console.WriteLine(str)
        End If
    End Sub
End Module
