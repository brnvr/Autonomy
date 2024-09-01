import { forwardRef, useImperativeHandle, useState } from "react";
import Button from "../Buttons/Button";
import Modal, { ModalProps } from "./Modal"

interface ModalFormProps extends ModalProps {
    saveButtonText?:string,
    onSubmit?:(e?:any)=>void,
    onClose?:()=>void
}

const ModalForm = (props:ModalFormProps) => {
    const onSubmit = e => {
        e.preventDefault()
        props.onSubmit && props.onSubmit(e)
    }

    const onClose = ()=> {
        props.onClose && props.onClose()
    }
    
    const content = (
        <form onSubmit={onSubmit} method="POST">
            {props.content}
            <div style={{display:'flex', justifyContent:'space-between'}}>
                <div style={{display:'inline-block', width:200}}>
                    <Button content={"Cancel"} border={true} onClick={onClose} />
                </div>
                <div style={{display:'inline-block', width:200}}>
                    <Button type="submit" content={props.saveButtonText || "Save"} />
                </div>
            </div>
        </form>
    )

    return props.visible && (
        <Modal loading={props.loading} visible={props.visible} width={props.width} height={props.height} content={content} />
    )
}

export default ModalForm;