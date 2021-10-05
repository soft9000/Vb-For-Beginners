
' Demonstrate how to use a Property.
'
Imports System

Module Module1
    Public iActualAge As Short

    Property iAge() As Short
        Get ' Get provides 
            Return iActualAge
        End Get
        Set(ByVal Value As Short) ' Set updates
            iActualAge = Value
        End Set
    End Property

    Sub Main()
        Console.WriteLine(iActualAge)
        iAge = 6
        Console.WriteLine(iAge)
        Console.WriteLine(iActualAge)
    End Sub
End Module ' Property1.vb

