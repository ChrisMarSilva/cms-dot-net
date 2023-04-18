import React from 'react';
//import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Login from './pages/Login';
import Alunos from './pages/Alunos';
import NovoAluno from './pages/NovoAluno'

let router = createBrowserRouter([
    { path: "/", element: <Login />, },
    { path: "/alunos", element: <Alunos />, },
    { path: "/alunos/novo/:alunoId", element: <NovoAluno />, },
]);

export default function Routes() {
    return <RouterProvider router={router} />;
}

// export default function Routes() {
//     return (
//         <BrowserRouter>
//             <Switch>
//                 <Route path="/" exact component={Login} />
//                 <Route path="/alunos" component={Alunos} />
//                 <Route path="/aluno/novo/:alunoId" component={NovoAluno} />
//             </Switch>
//         </BrowserRouter>
//     );
// }