import {createBrowserRouter, Navigate, RouteObject} from "react-router-dom";
import App from "../layout/App.tsx";
import PostDashboard from "../../feauture/posts/dashboard/PostDashboard.tsx";
import PostDetails from "../../feauture/posts/Details/PostDetails.tsx";
import LoginForm from "../../feauture/users/LoginForm.tsx";
import NotFound from "../errors/NotFound.tsx";
import ServerError from "../errors/ServerError.tsx";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App/>,
        children: [
            {path: 'posts', element: <PostDashboard/>},
            {path: 'posts/:id', element: <PostDetails/>},
            {path: 'login', element: <LoginForm/>},
            {path: 'not-found', element: <NotFound/>},
            {path: 'server-error', element: <ServerError/>},
            {path: '*', element: <Navigate replace to='/not-found'/>},
        ]
    }
]
export const router = createBrowserRouter(routes)