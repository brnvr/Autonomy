import { ReactNode, useEffect, useState } from "react"
import { BallTriangle } from 'react-loader-spinner'

export interface ModalProps {
    width?:any,
    height?:any,
    visible?:boolean,
    loading?:boolean,
    locked?:boolean,
    content:ReactNode,
    inactive?:boolean
}

const Modal = (props:ModalProps) => {
    const [showOverlay, setShowOverlay] = useState<boolean>(false)

    useEffect(() => {
        const overlays = document.querySelectorAll(".fullscreen-overlay")
        setShowOverlay(overlays.length == 1)
    }, [props.visible])

    return props.visible && (
        <div className="fullscreen-overlay" style={showOverlay ? {} : {backgroundColor:'transparent'}}>
            <div className={`modal ${props.inactive ? "inactive" : ""}`} style={{width: props.width, height: props.height}}>
                <div className="modal-content">
                    {props.content}
                    {props.loading && (<>
                        <div className="loading-overlay white"></div>
                        <BallTriangle color="#5361ca" wrapperClass="loader" />
                    </>)}
                    {props.locked && <div className="loading-overlay"></div>}
                </div>
            </div> 
        </div>
    )
}

export default Modal