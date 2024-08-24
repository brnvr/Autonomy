import React, { useState } from 'react';
import { BallTriangle } from 'react-loader-spinner'

interface Column {
    title:string,
    minWidth:string,
    render:(data:object) => any
}

interface DataTableProps {
    columns:Column[],
    fetch: () => Promise<any>
}

const centeredDivStyle: React.CSSProperties = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    zIndex: 10 
};

const overlayStyle: React.CSSProperties = {
    position: 'absolute',
    width: '100%',
    height: '100%',
    transform: 'translateY(-100%)',
    backgroundColor: 'rgba(255, 255, 255, 0.8)',
    zIndex: 5
}

const DataTable: React.FC<DataTableProps> = (props) => {
    const [rows, setRows] = useState<any[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const handleOnClick = () => {
        handleFetch()
        setIsLoading(true)
    }

    const handleFetch = () => props.fetch().then(rows => {
        setRows(rows)
        setIsLoading(false)
    })

    return (
        <div onClick={handleOnClick} className="rounded-sm border border-stroke bg-white px-5 pt-6 pb-2.5 shadow-default dark:border-strokedark dark:bg-boxdark sm:px-7.5 xl:pb-1">
            <div className="max-w-full overflow-x-auto" style={{position: 'relative'}}>
                <table className="w-full table-auto" style={{minHeight: '150px'}}>
                <thead>
                    <tr className="bg-gray-2 text-left dark:bg-meta-4">
                        {props.columns.map((column, index) => (
                            <th key={index} className={`min-w-[${column.minWidth}] py-4 px-4 font-medium text-black dark:text-white`}>
                                {column.title}
                            </th>
                        ))}
                    </tr>
                </thead>
                <tbody >
                    {rows.map((row, rowIndex) => (
                        <tr key={rowIndex}>
                            {props.columns.map((column, columnIndex) => (
                                <td key={columnIndex} className={`${rowIndex == rows.length-1 ? "" : "border-b"} border-[#eee] py-5 px-4 dark:border-strokedark`}>
                                    <p className="text-black dark:text-white">
                                        {column.render(row)}
                                    </p>
                                </td>
                            ))}
                        </tr>
                    ))}
                </tbody>
                </table>
                <div style={{...overlayStyle, display: isLoading ? "block" : "none"}}></div>
                <div style={{...centeredDivStyle, display: isLoading ? "block" : "none"}}>
                    <BallTriangle 
                        height="80"
                        width="80"
                        color="#5361ca"
                        ariaLabel="loading"
                    />
                </div>
            </div>
        </div>
    )
}

export default DataTable

