import Button from "../Buttons/Button";
import Modal, { ModalProps } from "./Modal"
import { Oval } from "react-loader-spinner";


interface ModalFormProps extends ModalProps {
    saveButtonText?:string
    cancelButtonText?:string
    onSubmit?:(e?:any)=>void
    submitting?:boolean
    onClose?:(e?:any)=>void
}

const ModalForm = (props:ModalFormProps) => {
    const onSubmit = e => {
        e.preventDefault()
        props.onSubmit && props.onSubmit(e)
    }

    const onClose = e => {
        props.onClose && props.onClose(e)
    }

    const content = <div style={{display:'flex', alignItems:'center', gap: 8}}>
        <span>{props.saveButtonText || "Save"}</span>
        {props.submitting && <>
            <Oval
                color="white"
                width='20px'
                height='20px'
                strokeWidth='3'
            />
        </>}
    </div>
    
    const modalContent = (
        <form onSubmit={onSubmit} method="POST">
            {props.content}
            <div style={{display:'flex', justifyContent:'space-between'}}>
                <div style={{display:'inline-block', width:200}}>
                    <Button color="blue" content={props.cancelButtonText || "Cancel"} border={true} onClick={onClose} />
                </div>
                <div style={{display:'inline-block', width:200}}>
                    <Button color="blue" type="submit" content={content} />
                </div>
            </div>
        </form>
    )

    return props.visible && (
        <div style={{position:'relative'}}>
            <Modal loading={props.loading} locked={props.submitting} visible={props.visible} width={props.width} height={props.height} content={modalContent} />
        </div>
    )
}

export default ModalForm;