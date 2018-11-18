Imports OpenTK.Graphics
Imports System.Collections.Generic

Namespace ThreeDlib.Figures


    Public MustInherit Class Figure
        Protected Dim isValid_ As Boolean = False
        Public Property IsEnable() As Boolean = True
        Public Property Color() As Color4
        Public Property Name() As String
        Private idConters_ As Dictionary(Of String, Integer)

        Public ReadOnly Property IsValid As Boolean 
            Get
                Return isValid_
            End Get
        End Property

        Public Sub New(color As Color4)
            Me.Color = color
        End Sub
            

        Protected MustOverride Function Init() As Boolean
        Public MustOverride Sub Draw()

        Public Function GetFigureType() As String 
            Dim type As String = Me.GetType().ToString()
            Dim dotInd As String = type.LastIndexOf(".")
            Return type.Substring(dotInd + 1, type.Length - dotInd - 1)
        End Function

        Public Overrides Function ToString() As String
            Dim type As String = Me.GetType().ToString()
            Dim dotInd As String = type.LastIndexOf(".")
            Return type.Substring(dotInd + 1, type.Length - dotInd - 1) + "Ololoev"
        End Function

    End Class

End Namespace

