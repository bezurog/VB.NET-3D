Imports System.Collections.Generic
Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib

    Public Class Service
        Public Shared Function Length(a As Vector4, b As Vector4) As Double
            Dim vect As Vector4 = a - b
            Return vect.Length
        End Function

        Public Shared Function IsParallel(a As Vector4, b As Vector4) As Boolean
            Dim koef As Double
            If b.X <> 0 Then koef = a.X / b.X
            If b.Y <> 0 Then koef = a.Y / b.Y
            If b.Z <> 0 Then koef = a.Z / b.Z
            Return a.X = b.X * koef And a.Y = b.Y * koef And a.Z = b.Z * koef
        End Function

        Public Shared Function GetArc(center As Vector4, start As Vector4, finish As Vector4, normal As Vector3, radius As Double) As List(Of Vector4)
            Dim list As List(Of Vector4) = New List(Of Vector4)()

            Dim x1, x2, y1, y2, x0, y0 As Double
            If normal.X <> 0 Then
                x0 = center.Y
                y0 = center.Z
                x1 = start.Y - x0
                x2 = finish.Y - x0
                y1 = start.Z - y0
                y2 = finish.Z - y0
            ElseIf normal.Y <> 0 Then

                x0 = center.X
                y0 = center.Z
                x1 = start.X - x0
                x2 = finish.X - x0
                y1 = start.Z - y0
                y2 = finish.Z - y0
            Else
                x0 = center.X
                y0 = center.Y
                x1 = start.X - x0
                x2 = finish.X - x0
                y1 = start.Y - y0
                y2 = finish.Y - y0
            End If

            Dim fi1 = Math.Atan(y1 / x1)
            If fi1 = 0 And (y1 < 0 Or x1 < 0) Then fi1 = Math.PI
            Dim fi2 = Math.Atan(y2 / x2)
            If fi2 = 0 And (y2 < 0 Or x2 < 0) Then fi2 = Math.PI
            Dim startFi As Double = If(fi1 < fi2, fi1, fi2)
            Dim finishFi As Double = If(fi1 > fi2, fi1, fi2)

            Dim x, y As Double
            Dim h As Double = 0.05
            Dim fi = startFi
            While fi <= finishFi
                x = radius * Math.Cos(fi) + x0
                y = radius * Math.Sin(fi) + y0
                fi += h
                If normal.X <> 0 Then
                    list.Add(New Vector4(center.X, x, y, 1))
                ElseIf normal.Y <> 0 Then
                    list.Add(New Vector4(x, center.Y, y, 1))
                Else
                    list.Add(New Vector4(x, y, center.Z, 1))
                End If
            End While
            Return list
        End Function

        Public Shared Function GetBezier2(p1 As Vector4, p2 As Vector4, p3 As Vector4) As List(Of Vector4)
            Dim list As List(Of Vector4) = New List(Of Vector4)()

            Dim t As Double = 0
            Dim h As Double = 0.05
            Dim x, y, z As Double
            While t <= 1
                x = Math.Pow(1 - t, 2) * p1.X + 2 * t * (1 - t) * p2.X + t * t * p3.X
                y = Math.Pow(1 - t, 2) * p1.Y + 2 * t * (1 - t) * p2.Y + t * t * p3.Y
                z = Math.Pow(1 - t, 2) * p1.Z + 2 * t * (1 - t) * p2.Z + t * t * p3.Z
                list.Add(New Vector4(x, y, z, 1))
                t += h
            End While
            list.Add(New Vector4(p3.X, p3.Y, p3.Z, 1))
            Return list
        End Function

        Public Shared Function IsEquals(p1 As Vector4, p2 As Vector4) As Boolean
            Dim eps As Double = 0.0001
            If Math.Abs(p1.X - p2.X) > eps Then Return False
            If Math.Abs(p1.Y - p2.Y) > eps Then Return False
            If Math.Abs(p1.Z - p2.Z) > eps Then Return False
            Return True
        End Function

        Public Shared Function CycleIndex(ind As Int32, size As Int32, inc As Boolean)
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
