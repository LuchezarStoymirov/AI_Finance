import { useState } from 'react'
import {createBrowserRouter, createRoutesFromElements, Route, Link, Outlet, RouterProvider} from 'react-router-dom'
import { Home } from './Pages/Home'
import { Profile } from './Pages/Profile'
import {  Data } from './Pages/Data'
import { Header } from './components/Header'

import './App.css'

function App(props) {

  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path='/' element={<Root />}>
          <Route index element={<Home/>}/>
          <Route path='/profile' element={<Profile/>}/>
          <Route path='/data' element={<Data/>}/>
      </Route>
    )
  )

  return (
      <div>
        <RouterProvider router = {router}/>
      </div>
  )
}

const Root = () => {
  return (
    <>
    <Header
    />

    <div>
      <Outlet />
    </div>
    </>
  );
}

export default App
