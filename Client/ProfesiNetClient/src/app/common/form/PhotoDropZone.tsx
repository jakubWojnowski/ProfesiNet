import {useCallback} from 'react'
import {useDropzone} from 'react-dropzone'

interface Props {
    setFile: (file: Blob | null) => void;
}
function PhotoDropZone() {
    const onDrop = useCallback((acceptedFiles:object[]) => {
        console.log(acceptedFiles);
    }, [])
    const {getRootProps, getInputProps, isDragActive} = useDropzone({onDrop})

    return (
        <div {...getRootProps()}>
            <input {...getInputProps()} />
            {
                isDragActive ?
                    <p>Drop the files here ...</p> :
                    <p>Drag 'n' drop some files here, or click to select files</p>
            }
        </div>
    )
}

export default PhotoDropZone