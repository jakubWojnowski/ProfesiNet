import {RouteObject, createBrowserRouter, Navigate} from 'react-router-dom';
import App from "../layout/App.tsx";
import RequireAuth from "../common/components/RequireAuth.tsx";
import PostDashboard from "../../feauture/posts/dashboard/PostDashboard.tsx";
import LoginForm from "../../feauture/users/LoginForm.tsx";
import NotFound from "../errors/NotFound.tsx";
import ServerError from "../errors/ServerError.tsx";
import ProfilePage from "../../feauture/profile/ProfilePage.tsx";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            {
                path: 'posts',
                element: (
                    <RequireAuth>
                        <PostDashboard />
                    </RequireAuth>
                ),
            },
            {
                path: 'profile/:username',
                element: (
                    <RequireAuth>
                        <ProfilePage />
                    </RequireAuth>
                ),
            },
            {
                path: 'login',
                element: <LoginForm />,
            },
            {
                path: 'not-found',
                element: <NotFound />,
            },
            {
                path: 'server-error',
                element: <ServerError />,
            },
            {
                path: '*',
                element: <Navigate replace to="/not-found" />,
            },
        ],
    },
];

export const router = createBrowserRouter(routes);
