import { Navigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const ProtectedRoute = ({ children, requireDonor = false, requireNGO = false }) => {
  const { isAuthenticated, isDonor, isNGO } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  if (requireDonor && !isDonor) {
    return <Navigate to="/" replace />;
  }

  if (requireNGO && !isNGO) {
    return <Navigate to="/" replace />;
  }

  return children;
};

export default ProtectedRoute;