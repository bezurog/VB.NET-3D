﻿Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures

    Public Class Line
        Inherits LineBasedFigure

        Public Property P1() As Vector4
        Public Property P2() As Vector4

        Public Property IsStipple() As Boolean

        Sub New(p1 As Vector4, p2 As Vector4, isStipple As Boolean, Optional lineWidth As Double = 1,
                Optional Name As String = Nothing) 

            MyBase.New(Color4.Black, lineWidth, Name)
            Me.P1 = p1
            Me.P2 = p2
            Me.IsStipple = isStipple
            Me.isValid_ = Init()
        End Sub

        Public Overrides Sub Draw()
            If Not IsEnable Then Return

            GL.LineWidth(LineWidth)
            If IsStipple Then
                GL.Enable(EnableCap.LineSmooth)
                GL.Enable(EnableCap.LineStipple)
                GL.LineStipple(2, 255)
            End If

            GL.Begin(BeginMode.Lines)
            GL.Color4(Color.Black)
            GL.Vertex4(p1)
            GL.Vertex4(p2)
            GL.End()

            If isStipple Then
                GL.Disable(EnableCap.LineSmooth)
                GL.Disable(EnableCap.LineStipple)
            End If

            GL.LineWidth(1)
        End Sub

        Protected Overrides Function Init() As Boolean
            Return P1.IsNotEquals(P2)
        End Function
    End Class
End  Namespace

