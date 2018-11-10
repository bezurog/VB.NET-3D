Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures


    Public Class Rect
        Inherits Figure

        Public Property P1() As Vector4
        Public Property P2() As Vector4
        Public Property P3() As Vector4
        Public Property P4() As Vector4

        Public Property IsBorders() As Boolean
        Public Property IsFaces() As Boolean
        Public Property Color() As Color

        Sub New(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, clr As Color, isBorders As Boolean, isFaces As Boolean)
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
            GL.Enable(EnableCap.Blend)
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha)

            If isBorders Then
                GL.Begin(BeginMode.LineLoop)
                GL.Color4(Color.Black)
                GL.Vertex4(p1)
                GL.Vertex4(p2)
                GL.Vertex4(p3)
                GL.Vertex4(p4)
                GL.End()
            End If

            If isFaces Then
                GL.Begin(BeginMode.Quads)
                GL.Color4(Color)
                GL.Vertex4(p1)
                GL.Vertex4(p2)
                GL.Vertex4(p3)
                GL.Vertex4(p4)
                GL.End()
            End If

            GL.Disable(EnableCap.Blend)
        End Sub

        Public ReadOnly Property IsValid As Boolean 
            Get
                Return P1 <> P2 And P1 <> P3 And P1 <> P4 And P2 <> P3 And P2 <> P4 And P3 <> P4
            End Get
        End Property

        Protected Overrides Function Init() As Boolean
             Return P1 <> P2 And P1 <> P3 And P1 <> P4 And P2 <> P3 And P2 <> P4 And P3 <> P4
        End Function
    End Class

End Namespace
