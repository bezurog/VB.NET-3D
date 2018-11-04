Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures
    Public Class Pipe
        Inherits Figure

        Const koefBend As Double = 2.5
        Dim vertexCount_ As Integer
        Dim isBorders_ As Boolean
        
        Dim isOxBasis_ As Boolean
        Dim isOyBasis_ As Boolean
        Dim isOzBasis_ As Boolean
       
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
        Public ReadOnly Property IsConnectedP1 As Boolean
            Get
                Return isConnectedP1_
            End Get
        End Property

        Dim isConnectedP2_ As Boolean
        Public ReadOnly Property IsConnectedP2 As Boolean
            Get
                Return isConnectedP2_
            End Get
        End Property

        Dim r_ As Double
        Public Property R As Double
            Get
                Return r_
            End Get
            Private Set(value As Double)
                r_ = value
            End Set
        End Property

        Dim fi_ As Double
        Public Property Fi As Double
            Get
                Return fi_
            End Get
            Private Set(value As Double)
                fi_ = value
            End Set
        End Property

        Dim basis_ As Vector3
        Public Property Basis As Vector3
            Get
                Return basis_
            End Get
            Private Set(value As Vector3)
                basis_ = value
            End Set
        End Property

        Dim color_ As Color
        Public Property Color As Color
            Get
                Return color_
            End Get
            Private Set(value As Color)
                color_ = value
            End Set
        End Property

        Dim vertexes1_() As Vector4
        Dim vertexes1Real_() As Vector4
        Public Property Vertexes1 As Vector4()
            Get
                Return vertexes1Real_
            End Get
            Private Set(value As Vector4())
                vertexes1Real_ = value
            End Set
        End Property

        Dim vertexes2_() As Vector4
        Dim vertexes2Real_() As Vector4
        Public Property Vertexes2 As Vector4()
            Get
                Return vertexes2Real_
            End Get
            Private Set(value As Vector4())
                vertexes2Real_ = value
            End Set
        End Property
        Public ReadOnly Property RadiusVector As Vector4
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

        Sub New(p1 As Vector4, p2 As Vector4)
            Me.P1 = p1
            Me.P2 = p2
        End Sub

        Public ReadOnly Property IsValid As Boolean
            Get
                Return Not (P1.X <> P2.X And P1.Y <> P2.Y Or P1.X <> P2.X And P1.Z <> P2.Z Or P1.Y <> P2.Y And P1.Z <> P2.Z)
            End Get
        End Property
            

        Private Sub CalcVertexes()
            ReDim vertexes1_(vertexCount_ - 1)
            ReDim vertexes2_(vertexCount_ - 1)
            ReDim vertexes1Real_(vertexCount_ - 1)
            ReDim vertexes2Real_(vertexCount_ - 1)

            For i = 0 To vertexCount_ - 1
                If Basis.Z = 1 Then
                    vertexes1_(i) = New Vector4(r_ * Math.Cos(i * fi_), r_ * Math.Sin(i * fi_), 0, 1)
                    vertexes2_(i) = New Vector4(r_ * Math.Cos(i * fi_), r_ * Math.Sin(i * fi_), P2.Z - P1.Z, 1)
                ElseIf Basis.X = 1 Then
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

        Public Sub Init(clr As Color, width As Double, vertexesCount As Integer, isBorders As Boolean)
            Dim i As Integer

            Me.isBorders_ = isBorders
            Me.vertexCount_ = vertexesCount
            Me.Color = clr
            Me.r_ = width / 2.0
            Me.fi_ = 2 * Math.PI / vertexCount_

            If P1.X = P2.X And P1.Y = P2.Y Then
                Me.Basis = New Vector3(0, 0, 1)
                isOzBasis_ = True
            ElseIf P1.Y = P2.Y And P1.Z = P2.Z Then
                Me.Basis = New Vector3(1, 0, 0)
                isOxBasis_ = True
            Else
                Me.Basis = New Vector3(0, 1, 0)
                isOyBasis_ = True
            End If

            CalcVertexes()
        End Sub

        Public Overrides Sub Draw()

            GL.PushMatrix()
            GL.Translate(P1.X, P1.Y, P1.Z)
            'GL.Rotate(90, Basis)

            GL.Color4(Color)

            GL.Begin(BeginMode.Polygon)
            For i = 0 To vertexCount_ - 1
                GL.Vertex4(vertexes1_(i))
            Next
            GL.End()

            GL.Color4(Color.Black)
            If (isBorders_) Then
                GL.Begin(BeginMode.LineLoop)
                For i = 0 To vertexCount_ - 1
                    GL.Vertex4(vertexes1_(i))
                Next
                GL.End()
            End If
            GL.Color4(Color)

            GL.Begin(BeginMode.Polygon)
            For i = 0 To vertexCount_ - 1
                GL.Vertex4(vertexes2_(i))
            Next
            GL.End()

            GL.Color4(Color.Black)
            If (isBorders_) Then
                GL.Begin(BeginMode.LineLoop)
                For i = 0 To vertexCount_ - 1
                    GL.Vertex4(vertexes2_(i))
                Next
                GL.End()
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

            GL.PopMatrix()
            GL.Color4(Color.Black)
            isDrawn = True



        End Sub

        Protected Function Cut1(len As Double) As Vector4
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

        Protected Function Cut2(len As Double) As Vector4
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


        Public Shared Function Connect(pipe1 As Pipe, pipe2 As Pipe, radius As Double) As Boolean
            If pipe1 Is Nothing Or pipe2 Is Nothing Then Return False
            If pipe1.Orto = pipe2.Orto Then Return False
            If pipe1.R <> pipe2.R Then Return False
            If pipe1.Vertexes1.Count <> pipe2.Vertexes1.Count Then Return False
            If radius <= pipe1.R Then Return False
            If pipe1.P1 <> pipe2.P1 And pipe1.P1 <> pipe2.P2 And pipe1.P2 <> pipe2.P1 And pipe1.P2 <> pipe2.P2 Then Return False

            Dim beginCenterPoint As Vector4
            Dim endCenterPoint As Vector4
            Dim vertexes1 As Vector4() = pipe1.Vertexes1
            Dim vertexes2 As Vector4() = pipe2.Vertexes1

            If pipe1.P1 = pipe2.P1 Then
                pipe1.Cut1(radius)
                pipe2.Cut1(radius)
                beginCenterPoint = pipe1.P1
                endCenterPoint = pipe2.P1
                vertexes1 = pipe1.Vertexes1
                vertexes2 = pipe2.Vertexes1
            ElseIf pipe1.P1 = pipe2.P2 Then
                pipe1.Cut1(radius)
                pipe2.Cut2(radius)
                beginCenterPoint = pipe1.P1
                endCenterPoint = pipe2.P2
                vertexes1 = pipe1.Vertexes1
                vertexes2 = pipe2.Vertexes2
            ElseIf pipe1.P2 = pipe2.P1 Then
                pipe1.Cut2(radius)
                pipe2.Cut1(radius)
                beginCenterPoint = pipe1.P2
                endCenterPoint = pipe2.P1
                vertexes1 = pipe1.Vertexes2
                vertexes2 = pipe2.Vertexes1
            Else
                pipe1.Cut2(radius)
                pipe2.Cut2(radius)
                beginCenterPoint = pipe1.P2
                endCenterPoint = pipe2.P2
                vertexes1 = pipe1.Vertexes2
                vertexes2 = pipe2.Vertexes2
            End If

            If beginCenterPoint = pipe1.P1 And pipe1.IsConnectedP1 Then Return False
            If beginCenterPoint = pipe1.P2 And pipe1.IsConnectedP2 Then Return False
            If endCenterPoint = pipe2.P1 And pipe2.IsConnectedP1 Then Return False
            If endCenterPoint = pipe2.P2 And pipe2.IsConnectedP2 Then Return False

            Dim len1 As Double = (endCenterPoint - beginCenterPoint).Length
            Dim minLen As Double = (vertexes1(0) - vertexes2(0)).Length 'Service.Length(vertexes1(0), vertexes2(0))
            Dim startIndex As Int32 = 0
            Dim endIndex As Int32 = 0
            Dim len As Double
            For i = 0 To vertexes1.Length - 1
                For j = 0 To vertexes2.Length - 1
                    len = (vertexes1(i) - vertexes2(j)).Length 'Service.Length(vertexes1(i), vertexes2(j))
                    If len < minLen Then
                        startIndex = i
                        endIndex = j
                        minLen = len
                    End If
                Next j
            Next i

            Dim nextVertex1 As Vector4 = vertexes1(CycleIndex(startIndex, vertexes1.Count(), True))
            Dim nextVertex2 As Vector4 = vertexes2(CycleIndex(endIndex, vertexes2.Count(), True))
            Dim nextVertex3 As Vector4 = vertexes2(CycleIndex(endIndex, vertexes2.Count(), False))
            Dim lenNext2 As Double = (nextVertex2 - nextVertex1).Length
            Dim lenNext3 As Double = (nextVertex3 - nextVertex1).Length
            Dim isInc As Boolean = lenNext2 < lenNext3

            Dim pipe12Orto As Vector3 = pipe1.Orto + pipe2.Orto
            Dim e As Vector3 = New Vector3(1, 1, 1)
            Dim normal As Vector3

            If pipe1.isOxBasis_ And pipe2.isOyBasis_ Or pipe1.isOyBasis_ And pipe2.isOxBasis_ Then
                normal = New Vector3(0, 0, 1)
            ElseIf pipe1.isOxBasis_ And pipe2.isOzBasis_ Or pipe2.isOxBasis_ And pipe1.isOzBasis_ Then
                normal = New Vector3(0, 1, 0)
            Else
                normal = New Vector3(1, 0, 0)
            End If

            Dim delta11 As Vector3 = (e - pipe1.Orto - normal) * radius
            Dim delta12 As Vector3 = (e - pipe1.Orto - normal) * (-radius)
            Dim delta21 As Vector3 = (e - pipe2.Orto - normal) * radius
            Dim delta22 As Vector3 = (e - pipe2.Orto - normal) * (-radius)

            Dim candidate11 As Vector4 = beginCenterPoint + New Vector4(delta11.X, delta11.Y, delta11.Z, 1)
            Dim candidate12 As Vector4 = beginCenterPoint + New Vector4(delta12.X, delta12.Y, delta12.Z, 1)
            Dim candidate21 As Vector4 = endCenterPoint + New Vector4(delta21.X, delta21.Y, delta21.Z, 1)
            Dim candidate22 As Vector4 = endCenterPoint + New Vector4(delta22.X, delta22.Y, delta22.Z, 1)

            Dim centerPoint As Vector4 = If(candidate11 = candidate21 Or candidate11 = candidate22, candidate11, candidate12)


            Dim curvePoints() As List(Of Vector4)
            ReDim curvePoints(vertexes1.Count() - 1)
            Dim centerCurve As Vector4
            Dim r As Double
            Dim tmp As Vector4
            For i = 0 To vertexes1.Count() - 1
                If normal.X <> 0 Then
                    centerCurve = New Vector4(vertexes1(startIndex).X, centerPoint.Y, centerPoint.Z, 1)
                    tmp = vertexes1(startIndex) - centerCurve
                    r = Math.Sqrt(tmp.Y * tmp.Y + tmp.Z * tmp.Z)
                ElseIf normal.Y <> 0 Then
                    centerCurve = New Vector4(centerPoint.X, vertexes1(startIndex).Y, centerPoint.Z, 1)
                    tmp = vertexes1(startIndex) - centerCurve
                    r = Math.Sqrt(tmp.X * tmp.X + tmp.Z * tmp.Z)
                Else
                    centerCurve = New Vector4(centerPoint.X, centerPoint.Y, vertexes1(startIndex).Z, 1)
                    tmp = vertexes1(startIndex) - centerCurve
                    r = Math.Sqrt(tmp.X * tmp.X + tmp.Y * tmp.Y)
                End If


                curvePoints(i) = Service.GetArc(centerCurve, vertexes1(startIndex), vertexes2(endIndex), normal, r)
                startIndex = CycleIndex(startIndex, vertexes1.Count(), True)
                endIndex = CycleIndex(endIndex, vertexes2.Count(), isInc)
            Next

            Dim startPipe As Pipe = pipe1
            Dim endPipe As Pipe = pipe2
            If Service.IsEquals(vertexes2(endIndex), curvePoints(0)(0)) Then
                startPipe = pipe2
                endPipe = pipe1
            End If

            For i = 0 To vertexes1.Count() - 2
                'GL.Color4(Color.FromArgb(shift * i, shift * i, shift * i))
                GL.Begin(BeginMode.QuadStrip)
                For j = 0 To curvePoints(i).Count - 1
                    If (j <= curvePoints(i).Count \ 2) Then GL.Color4(startPipe.Color) Else GL.Color4(endPipe.Color)
                    GL.Vertex4(curvePoints(i)(j))
                    GL.Vertex4(curvePoints(i + 1)(j))
                Next
                GL.End()
            Next
            GL.Begin(BeginMode.QuadStrip)
            For j = 0 To curvePoints(curvePoints.Length - 1).Count - 1
                If (j <= curvePoints(curvePoints.Length - 1).Count \ 2) Then GL.Color4(startPipe.Color) Else GL.Color4(endPipe.Color)
                GL.Vertex4(curvePoints(curvePoints.Length - 1)(j))
                GL.Vertex4(curvePoints(0)(j))
            Next
            GL.End()

            If Not pipe1.isDrawn Then pipe1.Draw()
            If Not pipe2.isDrawn Then pipe2.Draw()
            Return True
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

        Private Shared Function CycleIndex(ind As Int32, size As Int32, inc As Boolean)
            Dim resultInd As Int32 = ind
            If inc Then
                If resultInd = size - 1 Then resultInd = 0 Else resultInd = ind + 1
            Else
                If resultInd = 0 Then resultInd = size - 1 Else resultInd = ind - 1
            End If
            Return resultInd
        End Function
    End Class

End Namespace




