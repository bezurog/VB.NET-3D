Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures

    Public Class PipeConnector
        Inherits Figure
        
        Private pipe1_ As Pipe
        Private pipe2_ As Pipe
        Private startPipe_ As Pipe
        Private endPipe_ As Pipe
        Private vertexesCount_ As Int32

        Private curvePoints() As List(Of Vector4)

        Public Property Pipe1 As Pipe
            Get
                Return pipe1_ 
            End Get
            Private Set(value As Pipe)
                pipe1_ = Value
            End Set
        End Property
        Public Property Pipe2 As Pipe
            Get
                Return pipe2_ 
            End Get
            Private Set(value As Pipe)
                pipe2_ = Value
            End Set
        End Property

        Public Property Radius() As Double

        Sub New(pipe1 As Pipe, pipe2 As Pipe, radius As Double, Optional Name As String = Nothing) 

            MyBase.New(Color4.Black, Name)
            Me.Pipe1 = pipe1
            Me.Pipe2 = pipe2
            Me.Radius = radius
            isValid_ = Init()
            Name = pipe1.Name + "_" + pipe2.Name
        End Sub

        Protected Overrides Function Init() As Boolean
            If Pipe1 Is Nothing Or Pipe2 Is Nothing Then Return False
            If Pipe1.Orto = Pipe2.Orto Then Return False
            If Pipe1.R <> Pipe2.R Then Return False
            If Pipe1.Vertexes1.Count <> Pipe2.Vertexes1.Count Then Return False
            If radius <= Pipe1.R Then Return False
            If Pipe1.P1 <> Pipe2.P1 And Pipe1.P1 <> Pipe2.P2 And Pipe1.P2 <> Pipe2.P1 And Pipe1.P2 <> Pipe2.P2 Then Return False

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

            Dim nextVertex1 As Vector4 = vertexes1(Service.CycleIndex(startIndex, vertexes1.Count(), True))
            Dim nextVertex2 As Vector4 = vertexes2(Service.CycleIndex(endIndex, vertexes2.Count(), True))
            Dim nextVertex3 As Vector4 = vertexes2(Service.CycleIndex(endIndex, vertexes2.Count(), False))
            Dim lenNext2 As Double = (nextVertex2 - nextVertex1).Length
            Dim lenNext3 As Double = (nextVertex3 - nextVertex1).Length
            Dim isInc As Boolean = lenNext2 < lenNext3

            Dim pipe12Orto As Vector3 = pipe1.Orto + pipe2.Orto
            Dim e As Vector3 = New Vector3(1, 1, 1)
            Dim normal As Vector3

            If pipe1.IsOxBasis And pipe2.IsOyBasis Or pipe1.IsOyBasis And pipe2.IsOxBasis Then
                normal = New Vector3(0, 0, 1)
            ElseIf pipe1.IsOxBasis And pipe2.IsOzBasis Or pipe2.IsOxBasis And pipe1.IsOzBasis Then
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
                startIndex = Service.CycleIndex(startIndex, vertexes1.Count(), True)
                endIndex = Service.CycleIndex(endIndex, vertexes2.Count(), isInc)
            Next

            startPipe_ = pipe1
            endPipe_ = pipe2
            If Service.IsEquals(vertexes2(endIndex), curvePoints(0)(0)) Then
                startPipe_ = pipe2
                endPipe_ = pipe1
            End If
            vertexesCount_ = vertexes1.Count() - 2

            Return True
        End Function

        Public Overrides Sub Draw()
            If Not IsEnable Then Return

            GL.Enable(EnableCap.Blend)
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
            For i = 0 To vertexesCount_
                'GL.Color4(Color.FromArgb(shift * i, shift * i, shift * i))
                GL.Begin(BeginMode.QuadStrip)
                For j = 0 To curvePoints(i).Count - 1
                    If (j <= curvePoints(i).Count \ 2) Then GL.Color4(startPipe_.Color) Else GL.Color4(endPipe_.Color)
                    GL.Vertex4(curvePoints(i)(j))
                    GL.Vertex4(curvePoints(i + 1)(j))
                Next
                GL.End()
            Next
            GL.Begin(BeginMode.QuadStrip)
            For j = 0 To curvePoints(curvePoints.Length - 1).Count - 1
                If (j <= curvePoints(curvePoints.Length - 1).Count \ 2) Then GL.Color4(startPipe_.Color) Else GL.Color4(endPipe_.Color)
                GL.Vertex4(curvePoints(curvePoints.Length - 1)(j))
                GL.Vertex4(curvePoints(0)(j))
            Next
            GL.End()

            GL.Disable(EnableCap.Blend)
            Pipe1.Draw()
            Pipe2.Draw()
        End Sub
    End Class
End Namespace

