﻿Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing

Namespace ThreeDlib.Figures
    Public MustInherit Class BorderFaceFigure
        Inherits LineBasedFigure
        Public Property IsBorders() As Boolean
        Public Property IsFaces() As Boolean

        Sub New(clr As Color, isBorders As Boolean, isFaces As Boolean, lineWidth As Double, Optional Name As String = Nothing)
            MyBase.New(clr, lineWidth, Name)
            Me.IsBorders = isBorders 
            Me.IsFaces = isFaces
        End Sub
    End Class
End Namespace

