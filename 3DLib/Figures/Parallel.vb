Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures

    Public Class Parallel
        Inherits BorderFaceFigure

        Public Property P1() As Vector4
        Public Property P2() As Vector4
        Public Property P3() As Vector4
        Public Property P4() As Vector4
        Public Property P5() As Vector4
        Public Property P6() As Vector4
        Public Property P7() As Vector4
        Public Property P8() As Vector4



        Sub New(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, 
                p5 As Vector4, p6 As Vector4, p7 As Vector4, p8 As Vector4, clr As Color, 
                isBorders As Boolean, isFaces As Boolean, Optional lineWidth As Double = 1, Optional Name As String = Nothing)

            MyBase.New(clr, isBorders, isFaces, lineWidth, Name)
            Me.P1 = p1
            Me.P2 = p2
            Me.P3 = p3
            Me.P4 = p4
            Me.P5 = p5
            Me.P6 = p6
            Me.P7 = p7
            Me.P8 = p8
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
                GL.Enable(EnableCap.LineSmooth)

                GL.Begin(BeginMode.LineStrip)
                GL.Color4(Color.Black)
                GL.Vertex4(p1)
                GL.Vertex4(p2)
                GL.Vertex4(p4)
                GL.Vertex4(p3)
                GL.Vertex4(p1)
                GL.Vertex4(p7)
                GL.Vertex4(p8)
                GL.Vertex4(p6)
                GL.Vertex4(p5)
                GL.Vertex4(p7)
                GL.End()

                GL.Begin(BeginMode.LineLoop)
                GL.Vertex4(p5)
                GL.Vertex4(p6)
                GL.Vertex4(p8)
                GL.Vertex4(p7)
                GL.End()

                GL.Begin(BeginMode.LineLoop)
                GL.Vertex4(p1)
                GL.Vertex4(p2)
                GL.Vertex4(p4)
                GL.Vertex4(p3)
                GL.End()

                GL.Begin(BeginMode.Lines)
                GL.Vertex4(p1)
                GL.Vertex4(p7)
                GL.Vertex4(p2)
                GL.Vertex4(p8)
                GL.Vertex4(p3)
                GL.Vertex4(p5)
                GL.Vertex4(p4)
                GL.Vertex4(p6)
                GL.End()
                GL.LineWidth(1)
            End If

            If isFaces Then
                GL.Color4(Color)
                GL.Begin(BeginMode.QuadStrip)
                GL.Vertex4(p1)
                GL.Vertex4(p2)
                GL.Vertex4(p3)
                GL.Vertex4(p4)
                GL.Vertex4(p5)
                GL.Vertex4(p6)
                GL.Vertex4(p7)
                GL.Vertex4(p8)
                GL.Vertex4(p1)
                GL.Vertex4(p2)
                GL.End()

                GL.Begin(BeginMode.Quads)
                GL.Color4(Color)
                GL.Vertex4(p2)
                GL.Vertex4(p4)
                GL.Vertex4(p6)
                GL.Vertex4(p8)
                GL.Vertex4(p1)
                GL.Vertex4(p3)
                GL.Vertex4(p5)
                GL.Vertex4(p7)
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
            Dim points(8) As Vector4
            points(0) = P1
            points(1) = p2
            points(2) = p3
            points(3) = p4
            points(4) = p5
            points(5) = p6
            points(6) = p7
            points(7) = p8

            Dim i, j As Integer
            For i = 0 To points.Length - 2
                For j = i + 1 To points.Length - 1
                    If Service.IsEquals(points(i), points(j)) Then Return False
                Next j
            Next i
            Return True
        End Function
    End Class
End Namespace
