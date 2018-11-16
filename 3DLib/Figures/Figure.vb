﻿Imports OpenTK.Graphics

Namespace ThreeDlib.Figures


    Public MustInherit Class Figure
        Protected Dim isValid_ As Boolean = False
        Public Property IsEnable() As Boolean = True
        Public Property Color() As Color4

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

        Public Overrides Function ToString() As String
            Dim type As String = Me.GetType().ToString()
            Dim dotInd As String = type.LastIndexOf(".")
            Return type.Substring(dotInd + 1, type.Length - dotInd - 1)
        End Function

    End Class

End Namespace

