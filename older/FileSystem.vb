' FileSystem: R&D Version 0
' This program shows how to work with a file system
Module Module1
    Sub Main()
        Dim ThePath As String = "c:\"
        Dim TheVolume As String = FileSystem.Dir(ThePath, FileAttribute.Volume)
        Console.WriteLine("The volume is [" & TheVolume & "]")
        FileSystem.Dir(ThePath)

        Dim TheName As String = Dir(ThePath, vbDirectory)
        Do While TheName <> ""
            If (GetAttr(ThePath & TheName) And vbDirectory) = vbDirectory Then
                ' Display TheName only if it is a directory.
                Console.WriteLine("   Directory " & ThePath & TheName)
            End If
            ' Get next file system entry.
            TheName = Dir()
        Loop
    End Sub
End Module ' FileSystem.vb

