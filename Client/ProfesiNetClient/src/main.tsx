import React from 'react'
import ReactDOM from 'react-dom/client'
import './app/layout/styles.css'
import 'react-toastify/dist/ReactToastify.min.css'
import 'semantic-ui-css/semantic.min.css'
import {store, StoreContext} from "./app/stores/Store.ts";
import {RouterProvider} from "react-router-dom";
import {router} from "./app/router/Routes.tsx";
import 'react-datepicker/dist/react-datepicker.css';


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <StoreContext.Provider value={store}>
          <RouterProvider router={router}/>
      </StoreContext.Provider>
  
  </React.StrictMode>,
)
