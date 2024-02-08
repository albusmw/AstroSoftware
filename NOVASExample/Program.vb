Imports System
Imports ASCOM.Tools
Imports ASCOM.Tools.Novas31

Module Program
    Sub Main(args As String())
        Dim novas As Novas
        Dim julianDate, deltaT, targetRa, targetDeclination, targetHeight As Double
        Dim rc As Short
        Dim target As Object3
        Dim observer As OnSurface

        novas = New Novas

        julianDate = Utilities.JulianDateUtc

        target = New Object3 With {
            .Name = "Jupiter",
            .Type = ObjectType.MajorPlanetSunOrMoon,
            .Number = Body.Jupiter
        }

        deltaT = Utilities.DeltaT(julianDate)

        observer.Temperature = 10.0
        observer.Pressure = 1010
        observer.Latitude = 51.0 ' CHange to match your observatory's latitude, longitude and height above sea level
        observer.Longitude = 0.0
        observer.Height = 80

        rc = Novas.TopoPlanet(julianDate, target, deltaT, observer, Accuracy.Full, targetRa, targetDeclination, targetHeight)

        Console.WriteLine($"Jupiter coordinates - RA: {Utilities.HoursToHMS(targetRa)}, Declination: {Utilities.DegreesToDMS(targetDeclination)}")
        Console.ReadKey()

    End Sub
End Module
