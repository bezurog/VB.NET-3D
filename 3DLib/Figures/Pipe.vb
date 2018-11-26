Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures
    Public Class Pipe
        Inherits LineBasedFigure

        Const koefBend As Double = 2.5
        Private vertexCount_ As Integer
        Private isBorders_ As Boolean
        Private fi_ As Double
        Private basis_ As Vector3

        Private vertexes1_() As Vector4
        Private vertexes2_() As Vector4
        
        #Region  "Props"

        Dim p1_ As Vector4
        Public Property P1 As Vector4
            Get
                Return p1_
            End Get
            Private Set(value As Vector4)
                p1_ = value
            End Set
        End Property

        Dim p2_ As Vector4
        Public Property P2 As Vector4
            Get
                Return p2_
            End Get
            Private Set(value As Vector4)
                p2_ = value
            End Set
        End Property

        Dim isConnectedP1_ As Boolean
        Friend ReadOnly Property IsConnectedP1 As Boolean
            Get
                Return isConnectedP1_
            End Get
        End Property

        Dim isConnectedP2_ As Boolean
        Friend ReadOnly Property IsConnectedP2 As Boolean
            Get
                Return isConnectedP2_
            End Get
        End Property

        Dim r_ As Double
        Friend Property R As Double
            Get
                Return r_
            End Get
            Private Set(value As Double)
                r_ = value
            End Set
        End Property

        Dim isOxBasis_ As Boolean
        Friend Property IsOxBasis As Boolean
            Get
                Return isOxBasis_
            End Get
            Private Set(value As Boolean)
                isOxBasis_ = value
            End Set
        End Property

        Dim isOyBasis_ As Boolean
        Friend Property IsOyBasis As Boolean
            Get
                Return isOyBasis_
            End Get
            Private Set(value As Boolean)
                isOyBasis_ = value
            End Set
        End Property

        Dim isOzBasis_ As Boolean
        Friend Property IsOzBasis As Boolean
            Get
                Return isOzBasis_
            End Get
            Private Set(value As Boolean)
                isOzBasis_ = value
            End Set
        End Property


        Dim vertexes1Real_() As Vector4
        Friend Property Vertexes1 As Vector4()
            Get
                Return vertexes1Real_
            End Get
            Private Set(value As Vector4())
                vertexes1Real_ = value
            End Set
        End Property

        Dim vertexes2Real_() As Vector4
        Friend Property Vertexes2 As Vector4()
            Get
                Return vertexes2Real_
            End Get
            Private Set(value As Vector4())
                vertexes2Real_ = value
            End Set
        End Property
        Friend ReadOnly Property RadiusVector As Vector4
            Get
                Return P2 - P1
            End Get
        End Property
        Public ReadOnly Property Orto As Vector3
            Get
                Dim i As Vector3 = New Vector3(1, 0, 0)
                Dim j As Vector3 = New Vector3(0, 1, 0)
                Dim k As Vector3 = New Vector3(0, 0, 1)
                If RadiusVector.X <> 0 Then Return i
                If RadiusVector.Y <> 0 Then Return j
                Return k
            End Get
        End Property

        #End Region

        Sub New(p1 As Vector4, p2 As Vector4, clr As Color, width As Double, vertexesCount As Integer, 
                isBorders As Boolean, Optional lineWidth As Double = 1, Optional Name As String = Nothing)

            MyBase.New(clr, lineWidth, Name)
            Me.P1 = p1
            Me.P2 = p2
            Me.Color = clr
            Me.vertexCount_ = vertexesCount
            Me.r_ = width / 2.0
            Me.fi_ = 2 * Math.PI / vertexCount_
            Me.isBorders_ = isBorders
            isValid_ = Init()
        End Sub

        Private Sub CalcVertexes()
            ReDim vertexes1_(vertexCount_ - 1)
            ReDim vertexes2_(vertexCount_ - 1)
            ReDim vertexes1Real_(vertexCount_ - 1)
            ReDim vertexes2Real_(vertexCount_ - 1)

            For i = 0 To vertexCount_ - 1
                If basis_.Z = 1 Then
                    vertexes1_(i) = New Vector4(r_ * Math.Cos(i * fi_), r_ * Math.Sin(i * fi_), 0, 1)
                    vertexes2_(i) = New Vector4(r_ * Math.Cos(i * fi_), r_ * Math.Sin(i * fi_), P2.Z - P1.Z, 1)
                ElseIf basis_.X = 1 Then
                    vertexes1_(i) = New Vector4(0, r_ * Math.Cos(i * fi_), r_ * Math.Sin(i * fi_), 1)
                    vertexes2_(i) = New Vector4(P2.X - P1.X, r_ * Math.Cos(i * fi_), r_ * Math.Sin(i * fi_), 1)
                Else
                    vertexes1_(i) = New Vector4(r_ * Math.Cos(i * fi_), 0, r_ * Math.Sin(i * fi_), 1)
                    vertexes2_(i) = New Vector4(r_ * Math.Cos(i * fi_), P2.Y - P1.Y, r_ * Math.Sin(i * fi_), 1)
                End If
            Next

            Dim tmp As Vector4
            For i = 0 To vertexCount_ - 1
                tmp = P1 + vertexes1_(i)
                vertexes1Real_(i) = New Vector4(tmp.X, tmp.Y, tmp.Z, 1)
                tmp = P1 + vertexes2_(i)
                vertexes2Real_(i) = New Vector4(tmp.X, tmp.Y, tmp.Z, 1)
            Next

        End Sub

        Protected Overrides Function Init() As Boolean
            Dim i As Integer
            If P1.X <> P2.X And P1.Y <> P2.Y Or P1.X <> P2.X And P1.Z <> P2.Z Or P1.Y <> P2.Y And P1.Z <> P2.Z Then Return False

            If P1.X = P2.X And P1.Y = P2.Y Then
                basis_ = New Vector3(0, 0, 1)
                isOzBasis_ = True
            ElseIf P1.Y = P2.Y And P1.Z = P2.Z Then
                basis_ = New Vector3(1, 0, 0)
                isOxBasis_ = True
            Else
                basis_ = New Vector3(0, 1, 0)
                isOyBasis_ = True
            End If

            CalcVertexes()
            Return True 
        End Function

        Public Overrides Sub Draw()
            If Not IsEnable Then Return

            GL.PushMatrix()
            GL.Translate(P1.X, P1.Y, P1.Z)
            'GL.Rotate(90, Basis)

            GL.Enable(EnableCap.Blend)
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)

            GL.Color4(Color)

            GL.Begin(BeginMode.Polygon)
            For i = 0 To vertexCount_ - 1
                GL.Vertex4(vertexes1_(i))
            Next
            GL.End()

            GL.Color4(Color.Black)
            If (isBorders_) Then
                GL.LineWidth(Me.LineWidth)
                GL.Begin(BeginMode.LineLoop)
                For i = 0 To vertexCount_ - 1
                    GL.Vertex4(vertexes1_(i))
                Next
                GL.End()
                GL.LineWidth(1)
            End If
            GL.Color4(Color)

            GL.Begin(BeginMode.Polygon)
            For i = 0 To vertexCount_ - 1
                GL.Vertex4(vertexes2_(i))
            Next
            GL.End()

            GL.Color4(Color.Black)
            If (isBorders_) Then
                GL.LineWidth(Me.LineWidth)
                GL.Begin(BeginMode.LineLoop)
                
                For i = 0 To vertexCount_ - 1
                    GL.Vertex4(vertexes2_(i))
                Next
                GL.End()
                GL.LineWidth(1)
            End If
            GL.Color4(Color)

            GL.Begin(BeginMode.QuadStrip)
            For i = 0 To vertexCount_ - 1
                GL.Vertex4(vertexes1_(i))
                GL.Vertex4(vertexes2_(i))
            Next
            GL.Vertex4(vertexes1_(0))
            GL.Vertex4(vertexes2_(0))
            GL.End()

            GL.Disable(EnableCap.Blend)
            GL.PopMatrix()
            GL.Color4(Color.Black)

        End Sub

        Friend Function Cut1(len As Double) As Vector4
            If isOxBasis_ Then
                If (P2.X < P1.X) Then len = -len
                P1 = New Vector4(P1.X + len, P1.Y, P1.Z, 1)
            ElseIf isOyBasis_ Then
                If (P2.Y < P1.Y) Then len = -len
                P1 = New Vector4(P1.X, P1.Y + len, P1.Z, 1)
            Else
                If (P2.Z < P1.Z) Then len = -len
                P1 = New Vector4(P1.X, P1.Y, P1.Z + len, 1)
            End If
            CalcVertexes()
        End Function

        Friend Function Cut2(len As Double) As Vector4
            If isOxBasis_ Then
                If (P1.X < P2.X) Then len = -len
                P2 = New Vector4(P2.X + len, P2.Y, P2.Z, 1)
            ElseIf isOyBasis_ Then
                If (P1.Y < P2.Y) Then len = -len
                P2 = New Vector4(P2.X, P2.Y + len, P2.Z, 1)
            Else
                If (P1.Z < P2.Z) Then len = -len
                P2 = New Vector4(P2.X, P2.Y, P2.Z + len, 1)
            End If
            CalcVertexes()
        End Function

        Private Shared Function GetThirdBezierPoint(p1 As Vector4, p2 As Vector4, directVector As Vector4) As Vector4
            Dim dist As Double
            Dim center As Vector4
            Dim result As Vector4
            If Math.Abs(p1.X - p2.X) < 0.001 Then
                center = New Vector4(p1.X, (p1.Y + p2.Y) / 2, (p1.Z + p2.Z) / 2, 1)
                result = New Vector4(center.X, center.Y + directVector.Y, center.Z + directVector.Z, 1)
            ElseIf Math.Abs(p1.Y - p2.Y) < 0.001 Then
                center = New Vector4((p1.X + p2.X) / 2, p1.Y, (p1.Z + p2.Z) / 2, 1)
                result = New Vector4(center.X + directVector.X, center.Y, center.Z + directVector.Z, 1)
            Else
                center = New Vector4((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2, p1.Z, 1)
                result = New Vector4(center.X + directVector.X, center.Y + directVector.Y, center.Z, 1)
            End If
            Return New Vector4(result.X, result.Y, result.Z, 1)

        End Function

    End Class

End Namespace




