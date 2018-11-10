Namespace ThreeDlib.Figures


    Public MustInherit Class Figure
        Protected Dim isValid_ As Boolean = False
        Public Property IsEnable() As Boolean = True

        Public ReadOnly Property IsValid As Boolean 
            Get
                Return isValid_
            End Get
        End Property

        Protected MustOverride Function Init() As Boolean
        Public MustOverride Sub Draw()
    End Class

End Namespace

