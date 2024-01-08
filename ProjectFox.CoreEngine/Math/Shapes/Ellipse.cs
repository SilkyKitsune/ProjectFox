/*
struct circle/ellipse (not a polytope)
    vector center
    int radius
    int eccentricity//pos for wide, neg for tol
    float rotation

    center => center
    points => { center, (radius, eccentricity) }

    Intersecting
      If (eccentricity=0)
        Return center.distance(point) <= radius
      Else
        Point.rotate(center, -rotation)
        //Use angle in relation to distance to center
*/
