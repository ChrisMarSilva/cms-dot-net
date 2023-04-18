// import { useState } from "react";
// import { useQuery } from "react-query";
// import { api } from "../../api/api";
// import { Aluno } from "../../models/AlunoModel";
// import { EditModal } from "../EditModal/EditModal";
// import { AlunoCard } from "../AlunoCard/AlunoCard";
// import { Container } from "./styles";
// export function UserList() {

// const [selectedUser, setSelectedUser] = useState<Aluno | null>(null);
// const { data, isError, isLoading } = useQuery("user-list", api.getUsers);

// <Container>
//     <h1>Lista de Usuários</h1>
//     {isLoading && <h3>Carregando...</h3>}
//     {isError && <h3>Ocorreu algum problema :(</h3>}
//     {data?.map((aluno) => (
//         <AlunoCard aluno={aluno} onClickEdit={() => setSelectedUser(aluno)} />
//     ))}
//     {selectedUser && (
//         <EditModal
//             user={selectedUser}
//             show={!!selectedUser}
//             handleClose={() => setSelectedUser(null)}
//         />
//     )}
// </Container>

//     render() ==> <div />;
// }
// }

import React, { Component } from 'react';

class UserList extends Component {

    constructor(props) {
        super(props);
        this.state = {};
    }

    render() {
        return (

            <div>

            </div>

        );
    }
}

export default UserList;