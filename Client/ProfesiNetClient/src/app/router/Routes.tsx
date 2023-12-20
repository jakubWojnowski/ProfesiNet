import {createBrowserRouter, RouteObject} from "react-router-dom";
import App from "../layout/App.tsx";
import HomePage from "../../feauture/home/HomePage.tsx";
import PostDashboard from "../../feauture/posts/dashboard/PostDashboard.tsx";
import PostDetails from "../../feauture/posts/Details/PostDetails.tsx";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App/>,
        children: [
            {path: '', element: <HomePage/>},
            {path: 'posts', element: <PostDashboard/>},
            {path: 'posts/:id', element: <PostDetails/>}
            ]
    }
]
export const router = createBrowserRouter(routes)