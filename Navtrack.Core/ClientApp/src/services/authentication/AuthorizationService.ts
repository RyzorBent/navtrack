import { UserRole } from "../../apis/types/user/UserRole";
import { AppContextAccessor } from "../appContext/AppContextAccessor";

export const AuthorizationService = {
  isAuthorized: (userRole?: UserRole) => {
    var appContext = AppContextAccessor.getAppContext();

    return (
      appContext.authenticationInfo.authenticated 
      // &&
      // (userRole === undefined ||
      //   (appContext.user !== undefined && appContext.user.role === userRole))
    );
  }
};
