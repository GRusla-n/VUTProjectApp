import React, { useState, useEffect } from 'react';
import { Button, Grid, Header } from 'semantic-ui-react';
import ImageCropper from './imageCropper';
import ImageDropzone from './imageDropzone';

interface Props {
  loading: boolean;
  uploadImage: (file: Blob) => void;
}

export default function ImageUpload({ loading, uploadImage }: Props) {
  const [file, setFile] = useState<any>([]);
  const [cropper, setCropper] = useState<Cropper>();

  function onCrop() {
    if (cropper) {
      cropper.getCroppedCanvas().toBlob((blob) => uploadImage(blob!));
    }
  }

  useEffect(() => {
    return () => {
      file.forEach((file: any) => URL.revokeObjectURL(file.preview));
    };
  }, [file]);

  return (
    <Grid>
      <Grid.Row>
        <Grid.Column width={4}>
          <Header sub color="teal" content="Step #1 - Add Photo" />
          <ImageDropzone setFile={setFile} />
        </Grid.Column>
        <Grid.Column width={1} />
        <Grid.Column width={4}>
          <Header sub color="teal" content="Step #2 - Add Photo" />
          {file && file.length > 0 && (
            <ImageCropper
              setCropper={setCropper}
              imagePreview={file[0].preview}
            />
          )}
        </Grid.Column>
      </Grid.Row>
      <Grid.Row>
        <Grid.Column width={1} />
        <Grid.Column width={4}>
          <Header sub color="teal" content="Step #2 - Add Photo" />
          {file && file.length > 0 && (
            <>
              <div
                className="img-preview"
                style={{ minHeight: 100, overflow: 'hidden' }}
              />
              <Button.Group>
                <Button
                  loading={loading}
                  onClick={onCrop}
                  positive
                  icon="check"
                  type="button"
                />
                <Button
                  disabled={loading}
                  onClick={() => setFile([])}
                  icon="close"
                />
              </Button.Group>
            </>
          )}
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
}
