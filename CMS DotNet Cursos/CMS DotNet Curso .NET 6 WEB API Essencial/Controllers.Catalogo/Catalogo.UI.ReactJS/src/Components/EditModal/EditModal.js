// import { useState } from "react";
// import { Modal, Button, Form } from "react-bootstrap";
// import { useMutation, useQueryClient } from "react-query";
// import { api } from "../../api/api";
// import { Aluno } from "./../../models/AlunoModel";

// interface Props {
//     show: boolean;
//     aluno: Aluno;
//     handleClose: () => void;
// }
// export function EditModal({ show, handleClose, aluno }: Props) {
//     // const [value, setValue] = useState(aluno.name);

//     // const queryClient = useQueryClient();

//     // const { isLoading, mutate } = useMutation(
//     //     () => api.updateUserName(aluno.id, value),
//     //     {
//     //         onSuccess: () => {
//     //             queryClient.invalidateQueries("user-list");
//     //             handleClose();
//     //         },
//     //     }
//     // );

//     //     <Modal show={show} onHide={handleClose}>
//     //     <Modal.Header closeButton>
//     //         <Modal.Title>Atualizar Usuário</Modal.Title>
//     //     </Modal.Header>
//     //     <Modal.Body>
//     //         <Form.Label>Nome</Form.Label>
//     //         <Form.Control
//     //             onChange={(e) => setValue(e.target.value)}
//     //             type="text"
//     //             value={value}
//     //         />
//     //     </Modal.Body>
//     //     <Modal.Footer>
//     //         <Button variant="secondary" onClick={handleClose}>
//     //             Fechar
//     //         </Button>
//     //         <Button variant="primary" onClick={() => mutate()}>
//     //             {isLoading ? "Carrendo..." : "Salvar"}
//     //         </Button>
//     //     </Modal.Footer>
//     // </Modal>

//     return (
//         <div />
//     );
// }

import React, { Component } from 'react';

class EditModal extends Component {

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

export default EditModal;