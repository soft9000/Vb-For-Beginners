' TickTackTwist - A multi player game engine to support a variably sized
' board and a changable tick tack toe (Connections) goal condition. It can
' be used to demonstrate the creation of both graphical and non graphical multi
' user games.
' =============================================================================
' 06/25/2004 - Created as part of my book on VB.NET. 
'               Designed for Educational Use.
'                by Randall Nagy
' -----------------------------------------------------------------------------
' Designed for ease of understanding. More efficient versions could be available.
' Please address any comments and suggestions to me via Soft9000.com
' -----------------------------------------------------------------------------
'
Imports System
Imports Microsoft.VisualBasic
Imports System.Text


Module Module1

    Class TickTackTwist
        Private aryBoard(60, 60) As Integer ' This board resizable (today space is cheaper than time)
        Private iRange As Integer = 3       ' And you must connect this many!
        Private iWinner                     ' The place for the winnner
        Private iSquare = 12                ' The size of the board
        Private sGoalPattern As String = "xxx" ' Initil pattern for success

        Public Enum MoveResult
            YouWon
            NextPlayer
            TryAgain
        End Enum

        ' This property manages the number of connecitons that a winner must make
        Public Property Connections()
            Get
                Return iRange
            End Get
            Set(ByVal Value)
                ' Unless you are playing testing, even 7 is impossible to do ...
                If (Value > 7) Then Value = 7
                iRange = Value

                ' Create a new goal pattern here
                Dim sb As New StringBuilder
                sb.EnsureCapacity(iRange)
                Dim ss As Integer
                For ss = 1 To iRange
                    sb.Append("x")
                Next
                sGoalPattern = sb.ToString()

            End Set
        End Property

        Public Sub NewGame()
            aryBoard.Clear(aryBoard, 0, aryBoard.Length)
            iWinner = Nothing
        End Sub

        Public Function QueryBoard() As String()
            Dim aResult(iSquare - 1) As String
            Dim xx, xStart, yy, yStart, iLength, ss As Integer
            _Fixup(yStart)
            _Fixup(xStart)
            iLength = iSquare
            _Fixup(iLength)
            iLength = iLength - 1   ' zero based
            Dim sLine As String
            For yy = yStart To iLength
                For xx = xStart To iLength
                    Select Case (aryBoard(xx, yy))
                        Case Nothing
                            sLine = sLine + "-"
                        Case Else
                            sLine = sLine + CStr(aryBoard(xx, yy))
                    End Select
                Next
                aResult(ss) = sLine
                ss = ss + 1
                sLine = ""
            Next
            Return aResult
        End Function

        Public Sub DrawBoard()
            Console.WriteLine("")
            Console.WriteLine("TickTackTwist!")
            Console.WriteLine("")

            Dim aLines() As String = QueryBoard()
            Dim sLine As String
            Dim iLine As Integer
            Dim cCh As Char
            Console.Write("  :")
            For Each cCh In aLines(0)
                If (iLine > 9) Then iLine = 0
                Console.Write(iLine)
                iLine = iLine + 1
            Next

            iLine = 0
            Console.WriteLine()
            Console.WriteLine()

            For Each sLine In aLines
                If (iLine > 9) Then iLine = 0
                Console.Write("{0}: ", iLine)
                Console.WriteLine(sLine)
                iLine = iLine + 1
            Next
        End Sub

        Public Property BoardSize() As Integer
            Get
                Return iSquare
            End Get
            Set(ByVal Value As Integer)
                If (Value >= 25) Then
                    Value = 24
                End If
                iSquare = Value
            End Set
        End Property

        Private Sub _Fixup(ByRef val)
            val = val + iSquare
        End Sub

        Private Function _Fixdown(ByVal val) As Integer
            val = val - iSquare
            If (val < 0) Then val = 0
            Return val
        End Function

        Public Function Fixup(ByRef x, ByRef y) As Boolean
            If (x < 0) Then Return False
            If (y < 0) Then Return False
            If (x >= iSquare) Then Return False
            If (y >= iSquare) Then Return False

            ' Give us some breathing room
            _Fixup(x)
            _Fixup(y)
            Return True
        End Function

        ' This allows you to "take back" a move.
        Public Sub MoveReset(ByVal x As Integer, ByVal y As Integer)
            If (Fixup(x, y) = False) Then Return
            aryBoard(x, y) = Nothing
        End Sub


        ' This function returns true when you have a winner.
        Public Function Move(ByVal x As Integer, ByVal y As Integer, ByVal PlayerToken As Integer) As MoveResult
            If (Fixup(x, y) = False) Then Return MoveResult.TryAgain

            If (aryBoard(x, y) <> Nothing) Then Return MoveResult.TryAgain

            aryBoard(x, y) = PlayerToken

            Dim iSS, lo, hi As Integer

            ' Check the horizontal
            Dim iBigger = iRange
            Dim sPattern As String
            lo = x - iBigger
            hi = x + iBigger
            For iSS = lo To hi
                If (aryBoard(iSS, y) = PlayerToken) Then
                    sPattern = sPattern + "x"
                Else
                    sPattern = sPattern + "-"
                End If
            Next
            If (sPattern.IndexOf(sGoalPattern) <> -1) Then
                iWinner = PlayerToken
                Return MoveResult.YouWon
            End If

            ' Check the vertical
            sPattern = ""
            For iSS = lo To hi
                If (aryBoard(x, iSS) = PlayerToken) Then
                    sPattern = sPattern + "x"
                Else
                    sPattern = sPattern + "-"
                End If
            Next
            If (sPattern.IndexOf(sGoalPattern) <> -1) Then
                iWinner = PlayerToken
                Return MoveResult.YouWon
            End If

            ' Check the first diagonal - top down
            sPattern = ""
            For iSS = x - iBigger To x + iBigger
                If (aryBoard(iSS, iSS) = PlayerToken) Then
                    sPattern = sPattern + "x"
                Else
                    sPattern = sPattern + "-"
                End If
            Next
            If (sPattern.IndexOf(sGoalPattern) <> -1) Then
                iWinner = PlayerToken
                Return MoveResult.YouWon
            End If


            ' Check the second diagonal - left up
            sPattern = ""
            For iSS = iBigger To 0 Step -1
                ' Here is how you resove back to user coordinates
                ' Console.WriteLine("({0}, {1})", _Fixdown(x + iSS), _Fixdown(y - iSS))
                If (aryBoard(x + iSS, y - iSS) = PlayerToken) Then
                    sPattern = sPattern + "x"
                Else
                    sPattern = sPattern + "-"
                End If
            Next
            If (sPattern.IndexOf(sGoalPattern) <> -1) Then
                iWinner = PlayerToken
                Return MoveResult.YouWon
            End If

            Return MoveResult.NextPlayer
        End Function

        Public Function QueryWinner() As Integer
            Return iWinner
        End Function
    End Class

    Class TestCases
        Public Function tst_Connect3(ByRef zGame As TickTackTwist, ByRef sError As String) As Boolean
            zGame.Connections = 3
            zGame.BoardSize = 12

            Dim result As TickTackTwist.MoveResult
            Dim br As Boolean

            ' Check the vertical logic
            If (zGame.Move(0, 1, 7) <> zGame.MoveResult.NextPlayer) Then
                sError = "Vertical Error"
                GoTo WeDone
            End If

            If (zGame.Move(0, 2, 7) <> zGame.MoveResult.NextPlayer) Then
                sError = "Vertical Error"
                GoTo WeDone
            End If

            If (zGame.Move(0, 3, 7) <> zGame.MoveResult.YouWon) Then
                sError = "Vertical Error - I should have won!"
                GoTo WeDone
            End If

            ' Check the horizontal logic
            If (zGame.Move(1, 0, 6) <> zGame.MoveResult.NextPlayer) Then
                sError = "Horizontal Error"
                GoTo WeDone
            End If

            If (zGame.Move(2, 0, 6) <> zGame.MoveResult.NextPlayer) Then
                sError = "Horizontal Error"
                GoTo WeDone
            End If

            If (zGame.Move(3, 0, 6) <> zGame.MoveResult.YouWon) Then
                sError = "Horizontal Error - I should have won!"
                GoTo WeDone
            End If

            ' Check the left diagonal logic
            If (zGame.Move(1, 1, 5) <> zGame.MoveResult.NextPlayer) Then
                sError = "Left Diagonal Error"
                GoTo WeDone
            End If

            If (zGame.Move(2, 2, 5) <> zGame.MoveResult.NextPlayer) Then
                sError = "Left Diagonal Error"
                GoTo WeDone
            End If

            If (zGame.Move(3, 3, 5) <> zGame.MoveResult.YouWon) Then
                sError = "Left Diagonal Error - I should have won!"
                GoTo WeDone
            End If

            ' Check the left diagonal logic, odd fill
            If (zGame.Move(7, 7, 4) <> zGame.MoveResult.NextPlayer) Then
                sError = "Left Odd Diagonal Error"
                GoTo WeDone
            End If

            If (zGame.Move(9, 9, 4) <> zGame.MoveResult.NextPlayer) Then
                sError = "Left Odd Diagonal Error"
                GoTo WeDone
            End If

            If (zGame.Move(8, 8, 4) <> zGame.MoveResult.YouWon) Then
                sError = "Left Odd Diagonal Error - I should have won!"
                GoTo WeDone
            End If

            ' Check the right diagonal logic
            If (zGame.Move(6, 6, 3) <> zGame.MoveResult.NextPlayer) Then
                sError = "Right Diagonal Error"
                GoTo WeDone
            End If

            If (zGame.Move(5, 7, 3) <> zGame.MoveResult.NextPlayer) Then
                sError = "Right Diagonal Error"
                GoTo WeDone
            End If

            If (zGame.Move(4, 8, 3) <> zGame.MoveResult.YouWon) Then
                sError = "Right Diagonal Error - I should have won!"
                GoTo WeDone
            End If

            br = True
WeDone:
            Return br
        End Function

        Public Function tst_Max(ByRef zGame As TickTackTwist, ByRef sError As String) As Boolean
            Dim br As Boolean
            zGame.Connections = 100
            zGame.BoardSize = 100

            Dim ss As Integer

            ' First test case
            For ss = 1 To zGame.Connections
                If (zGame.Move(ss, ss, 1) <> TickTackTwist.MoveResult.NextPlayer) Then
                    If ((ss = zGame.Connections) And zGame.QueryWinner() = 1) Then
                        br = True
                    End If
                End If
            Next
            If (br = False) Then
                sError = "First test case fails."
                Return br
            End If

            ' Second test case
            Dim coord As Integer = (zGame.BoardSize / 2) - (zGame.Connections / 2)
            For ss = 1 To zGame.Connections
                If (zGame.Move(coord + ss, coord - ss, 2) <> TickTackTwist.MoveResult.NextPlayer) Then
                    If ((ss = zGame.Connections) And zGame.QueryWinner() = 1) Then
                        br = True
                    End If
                End If
            Next
            If (br = False) Then
                sError = "Second test case fails."
                Return br
            End If

            ' Third test case
            For ss = 1 To zGame.Connections
                If (zGame.Move(coord, coord - ss, 3) <> TickTackTwist.MoveResult.NextPlayer) Then
                    If ((ss = zGame.Connections) And zGame.QueryWinner() = 1) Then
                        br = True
                    End If
                End If
            Next
            If (br = False) Then
                sError = "Third test case fails."
                Return br
            End If

            ' Fourth test case
            For ss = 1 To zGame.Connections
                If (zGame.Move(coord + ss, coord, 4) <> TickTackTwist.MoveResult.NextPlayer) Then
                    If ((ss = zGame.Connections) And zGame.QueryWinner() = 1) Then
                        br = True
                    End If
                End If
            Next
            If (br = False) Then
                sError = "Fourth test case fails."
                Return br
            End If

            ' Fifth test case
            For ss = 1 To zGame.Connections
                If (zGame.Move(zGame.BoardSize - ss, zGame.BoardSize - ss, 5) <> TickTackTwist.MoveResult.NextPlayer) Then
                    If ((ss = zGame.Connections) And zGame.QueryWinner() = 1) Then
                        br = True
                    End If
                End If
            Next
            If (br = False) Then
                sError = "Fifth test case fails."
                Return br
            End If

            Return br
        End Function

        Public Function tst_Corners(ByRef zGame As TickTackTwist, ByRef sError As String) As Boolean
            Dim x, y As Integer
            zGame.MoveReset(x, y)
            If (zGame.Move(x, y, 1) <> TickTackTwist.MoveResult.NextPlayer) Then
                sError = "Upper left fails."
                Return False
            End If
            y = zGame.BoardSize - 1
            zGame.MoveReset(x, y)
            If (zGame.Move(x, y, 1) <> TickTackTwist.MoveResult.NextPlayer) Then
                sError = "Upper right fails."
                Return False
            End If
            x = zGame.BoardSize - 1
            zGame.MoveReset(x, y)
            If (zGame.Move(x, y, 1) <> TickTackTwist.MoveResult.NextPlayer) Then
                sError = "Lower right fails."
                Return False
            End If
            y = 0
            zGame.MoveReset(x, y)
            If (zGame.Move(x, y, 1) <> TickTackTwist.MoveResult.NextPlayer) Then
                sError = "Lower left fails."
                Return False
            End If
            Return True
        End Function

        Public Function tst_Sanity(ByRef zGame As TickTackTwist, ByRef sError As String) As Boolean
            Dim x, y As Integer

            ' Test for the set / noset / reset logic
            If (zGame.Move(0, 0, 1) <> TickTackTwist.MoveResult.NextPlayer) Then
                sError = "Bogus 1."
                Return False
            End If
            If (zGame.Move(0, 0, 1) <> TickTackTwist.MoveResult.TryAgain) Then
                sError = "Bogus 2."
                Return False
            End If
            zGame.MoveReset(0, 0)
            If (zGame.Move(0, 0, 1) <> TickTackTwist.MoveResult.NextPlayer) Then
                sError = "Bogus 3."
                Return False
            End If
            zGame.MoveReset(0, 0)

            ' Now overflow each corner ...
            y = -1
            x = -1
            If (zGame.Move(x, y, 1) <> TickTackTwist.MoveResult.TryAgain) Then
                sError = "Upper should have left failed."
                Return False
            End If
            y = zGame.BoardSize
            If (zGame.Move(x, y, 1) <> TickTackTwist.MoveResult.TryAgain) Then
                sError = "Upper should have right failed."
                Return False
            End If
            x = zGame.BoardSize
            If (zGame.Move(x, y, 1) <> TickTackTwist.MoveResult.TryAgain) Then
                sError = "Lower should have right failed."
                Return False
            End If
            y = 0
            If (zGame.Move(x, y, 1) <> TickTackTwist.MoveResult.TryAgain) Then
                sError = "Lower should have left failed."
                Return False
            End If
            Return True
        End Function

        Public Function Test(ByRef sError As String) As Boolean
            Dim zGame As New TickTackTwist

            ' First test case - test connect 3
            If (tst_Connect3(zGame, sError) = False) Then
                Return False
            End If

            zGame.NewGame()

            ' Next test case - push the limit
            If (tst_Max(zGame, sError) = False) Then
                Return False
            End If

            zGame.NewGame()

            ' Next test case - exceed the limit
            If (tst_Corners(zGame, sError) = False) Then
                Return False
            End If

            zGame.NewGame()

            ' Next test abuse case - overflow the limit
            If (tst_Sanity(zGame, sError) = False) Then
                Return False
            End If

            Return True
        End Function


    End Class

    Sub Main()
        Dim zTests As New TestCases
        Dim sError As String
        If (zTests.Test(sError) = False) Then
            Console.WriteLine(sError)
            Return
        End If

        Console.WriteLine("All tests passed!")

    End Sub



End Module
