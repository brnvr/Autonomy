import { ReactNode } from "react"
import { BallTriangle } from 'react-loader-spinner'

export interface ModalProps {
    width?:any,
    height?:any,
    visible?:boolean,
    loading?:boolean,
    content:ReactNode
}

const Modal = (props:ModalProps) => {
    return props.visible && (
        <div className="fullscreen-overlay">
            <div className="modal" style={{width: props.width, height: props.height}}>
                {props.content}
                {props.loading && (<>
                    <div className="loading-overlay white"></div>
                    <BallTriangle color="#5361ca" wrapperClass="loader" />
                </>)}
            </div>
            
        </div>
    )
}

export default Modal