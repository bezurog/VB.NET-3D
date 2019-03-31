﻿Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures


    Public Class Rect
        Inherits BorderFaceFigure

        Public Property P1() As Vector4
        Public Property P2() As Vector4
        Public Property P3() As Vector4
        Public Property P4() As Vector4

        Sub New(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, clr As Color, 
                isBorders As Boolean, isFaces As Boolean, Optional lineWidth As Double = 1, Optional Name As String = Nothing) 

            MyBase.New(clr, isBorders, isFaces, lineWidth, Name)
            Me.P1 = p1
            Me.P2 = p2
            Me.P3 = p3
            Me.P4 = p4
            Me.IsBorders = isBorders
            Me.IsFaces = isFaces
            Me.Color = clr
            Me.isValid_ = Init()
        End Sub

        Public Overrides Sub Draw()
            If Not IsEnable Then Return

            GL.Enable(EnableCap.Blend)
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)

            If isBorders Then
                GL.LineWidth(LineWidth)
                GL.Begin(BeginMode.LineLoop)
                GL.Color4(Color.Black)
                GL.Vertex4(p1)
                GL.Vertex4(p2)
                GL.Vertex4(p3)
                GL.Vertex4(P4)
                GL.End()
                GL.LineWidth(1)
            End If

            If isFaces Then
                GL.Begin(BeginMode.Quads)
                GL.Color4(Color)
                GL.Vertex4(p1)
                GL.Vertex4(p2)
                GL.Vertex4(p3)
                GL.Vertex4(P4)
                GL.End()
            End If

            GL.Disable(EnableCap.Blend)
        End Sub

        Public ReadOnly Property IsValid As Boolean 
            Get
                Return isValid_
            End Get
        End Property

        Protected Overrides Function Init() As Boolean
            Return Service.IsNotEquals(P1, P2) And Service.IsNotEquals(P1, P3) And Service.IsNotEquals(P1, P4) And
                Service.IsNotEquals(P2, P3) And Service.IsNotEquals(P2, P4) And Service.IsNotEquals(P3, P4)
        End Function
    End Class

End Namespace
