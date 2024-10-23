//This file is for protected routes that only certain users or users at all can access
//Changes route to login page when not logged in for a certain route
import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../Context/useAuth';

type Props = { children: React.ReactNode };

const ProtectedRoute = ({ children }: Props) => {
    //Need location to generate the URL
    const location = useLocation()
    const { isLoggedIn } = useAuth();

    return isLoggedIn() ? (
        <>{children}</> 
      ) : (
        <Navigate to="/login" state={{ from: location }} replace />
      );
};

export default ProtectedRoute