' TickTackTwister - A multi player game engine for tick tack toe
' 06/25/2004 - Created for Educational Use by Randall Nagy
'
' Please address your comments to soft9000.com
'
Imports System
Imports Microsoft.VisualBasic

Module Module1

    Class TickTackTwister
        Private aryBoard(50, 50) As Integer ' This board resizable
        Private iRange As Integer = 3       ' And you must connect this many!
        Private iWinner                     ' The place for the winnner
        Private iSquare = 12                ' The size of the board

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
                If (Value > 7) Then Value = 7
                iRange = Value
            End Set
        End Property

        Public Sub NewGame()
            aryBoard.Clear(aryBoard, 0, aryBoard.Length)
            iWinner = Nothing
        End Sub

        Public Function QueryBoard() As String()
            Dim aResult(iSquare) As String
            Dim xx, xStart, yy, yStart, iLength, ss As Integer
            _Fixup(yStart)
            _Fixup(xStart)
            iLength = iSquare
            _Fixup(iLength)
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
            Console.WriteLine("TickTackDoh!")
            Console.WriteLine("")

            Dim aLines() As String = QueryBoard()
            Dim sLine As String
            For Each sLine In aLines
                Console.WriteLine(sLine)
            Next
        End Sub

        Public Function SetPlayingSquare(ByVal isize As Integer) As Boolean
            If (iSquare >= 25) Then Return False
            iSquare = isize
            Return True
        End Function

        Private Sub _Fixup(ByRef val)
            val = val + iSquare + 1
        End Sub


        ' This function returns true when you have a winner.
        Public Function Move(ByVal x As Integer, ByVal y As Integer, ByVal PlayerToken As Integer) As MoveResult
            If (x > iSquare) Then Return MoveResult.TryAgain
            If (y > iSquare) Then Return MoveResult.TryAgain

            ' Give us some breathing room
            _Fixup(x)
            _Fixup(y)

            If (aryBoard(x, y) <> Nothing) Then Return MoveResult.TryAgain

            aryBoard(x, y) = PlayerToken

            Dim iSS As Integer

            ' Check the horizontal
            Dim sPattern As String
            For iSS = x - iRange To x + iRange
                If (aryBoard(iSS, y) = PlayerToken) Then
                    sPattern = sPattern + "x"
                Else
                    sPattern = sPattern + "-"
                End If
            Next
            If (sPattern.IndexOf("xxx") <> -1) Then
                iWinner = PlayerToken
                Return MoveResult.YouWon
            End If

            ' Check the vertical
            sPattern = ""
            For iSS = y - iRange To y + iRange
                If (aryBoard(x, iSS) = PlayerToken) Then
                    sPattern = sPattern + "x"
                Else
                    sPattern = sPattern + "-"
                End If
            Next
            If (sPattern.IndexOf("xxx") <> -1) Then
                iWinner = PlayerToken
                Return MoveResult.YouWon
            End If

            ' Check the first diagonal - top to bottom
            sPattern = ""
            For iSS = 0 To iRange * 2
                If (aryBoard(x + iSS, x + iSS) = PlayerToken) Then
                    sPattern = sPattern + "x"
                Else
                    sPattern = sPattern + "-"
                End If
            Next
            If (sPattern.IndexOf("xxx") <> -1) Then
                iWinner = PlayerToken
                Return MoveResult.YouWon
            End If


            ' Check the second diagonal - left to bottom
            sPattern = ""
            For iSS = iRange * 2 To 0 Step -1
                If (aryBoard(x - iSS, x - iSS) = PlayerToken) Then
                    sPattern = sPattern + "x"
                Else
                    sPattern = sPattern + "-"
                End If
            Next
            If (sPattern.IndexOf("xxx") <> -1) Then
                iWinner = PlayerToken
                Return MoveResult.YouWon
            End If

            Return MoveResult.NextPlayer
        End Function

        Public Function QueryWinner() As Integer
            Return iWinner
        End Function
    End Class

    Sub Main()

        Dim zgame As New TickTackTwister

        Dim result As TickTackTwister.MoveResult

        ' Check the vertical logic
        If (zgame.Move(0, 1, 7) <> zgame.MoveResult.NextPlayer) Then
            Console.WriteLine("Vertical Error")
            GoTo WeDone
        End If

        If (zgame.Move(0, 2, 7) <> zgame.MoveResult.NextPlayer) Then
            Console.WriteLine("Vertical Error")
            GoTo WeDone
        End If

        If (zgame.Move(0, 3, 7) <> zgame.MoveResult.YouWon) Then
            Console.WriteLine("Vertical Error - I should have won!")
            GoTo WeDone
        End If

        ' Check the horizontal logic
        If (zgame.Move(1, 0, 6) <> zgame.MoveResult.NextPlayer) Then
            Console.WriteLine("Vertical Error")
            GoTo WeDone
        End If

        If (zgame.Move(2, 0, 6) <> zgame.MoveResult.NextPlayer) Then
            Console.WriteLine("Vertical Error")
            GoTo WeDone
        End If

        If (zgame.Move(3, 0, 6) <> zgame.MoveResult.YouWon) Then
            Console.WriteLine("Horizontal Error - I should have won!")
            GoTo WeDone
        End If

        ' Check the left diagonal logic
        If (zgame.Move(1, 1, 5) <> zgame.MoveResult.NextPlayer) Then
            Console.WriteLine("Left Diagonal Error")
            GoTo WeDone
        End If

        If (zgame.Move(2, 2, 5) <> zgame.MoveResult.NextPlayer) Then
            Console.WriteLine("Left Diagonal Error")
            GoTo WeDone
        End If

        If (zgame.Move(3, 3, 5) <> zgame.MoveResult.YouWon) Then
            Console.WriteLine("Left Diagonal Error - I should have won!")
            GoTo WeDone
        End If

        ' Check the right diagonal logic
        If (zgame.Move(6, 6, 4) <> zgame.MoveResult.NextPlayer) Then
            Console.WriteLine("Right Diagonal Error")
            GoTo WeDone
        End If

        If (zgame.Move(5, 5, 4) <> zgame.MoveResult.NextPlayer) Then
            Console.WriteLine("Right Diagonal Error")
            GoTo WeDone
        End If

        If (zgame.Move(4, 4, 4) <> zgame.MoveResult.YouWon) Then
            Console.WriteLine("Right Diagonal Error - I should have won!")
            GoTo WeDone
        End If

        Console.WriteLine("Winner!")
WeDone:
        zgame.DrawBoard()
        zgame.NewGame()
    End Sub

End Module
