import { IconType } from "react-icons";
import Button from "../Buttons/Button";
import Modal, { ModalProps } from "./Modal"
import { Oval } from "react-loader-spinner";
import { getColorClass, TailwindColor } from "../../tailwind-utils";
import { ReactNode } from "react";

interface ModalConfirmProps {
    visible?:boolean
    loading?:boolean
    width?:number
    height?:number
    title?:ReactNode
    body:ReactNode
    icon?:IconType
    color:TailwindColor
    confirmButtonText?:string
    cancelButtonText?:string
    onConfirm?:(e?:any)=>void
    confirming?:boolean
    onClose?:(e?:any)=>void  
}

const ModalForm = (props:ModalConfirmProps) => {
    const onConfirm = e => {
        e.preventDefault()
        props.onConfirm && props.onConfirm(e)
    }

    const onClose = e => {
        props.onClose && props.onClose(e)
    }

    const content = <div style={{display:'flex', alignItems:'center', gap: 8}}>
        <span>{props.confirmButtonText || "Confirm"}</span>
        {props.confirming && <>
            <Oval
                secondaryColor="none"
                color="white"
                width='20px'
                height='20px'
                strokeWidth='3'
            />
        </>}
    </div>
    
    const modalContent = (
        <div>
            <div style={{display:'flex', flexDirection:'column', alignItems:'center'}}>
                {props.icon && (<props.icon className={getColorClass("bg", props.color)} style={{width:50, height:50}} />)}
                {props.title && (<div className="mt-5.5 pb-2 text-xl font-bold text-black dark:text-white sm:text-2xl">
                    {props.title}
                </div>)}
                <div style={{fontWeight: 500, marginBottom: 40}}>
                    {props.body}
                </div>
            </div>
            <div style={{display:'flex', justifyContent:'space-between'}}>
                <div style={{display:'inline-block', width:200}}>
                    <Button color="gray" content={props.cancelButtonText || "Cancel"} onClick={onClose} />
                </div>
                <div style={{display:'inline-block', width:200}}>
                    <Button color={props.color} content={content} onClick={onConfirm}/>
                </div>
            </div>
        </div>
    )

    return props.visible && (
        <div style={{position:'relative'}}>
            <Modal loading={props.loading} locked={props.confirming} visible={props.visible} width={props.width} height={props.height} content={modalContent} />
        </div>
    )
}

export default ModalForm;