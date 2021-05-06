import React, { useState, useEffect } from 'react';
import { Button, Grid, Header, Modal } from 'semantic-ui-react';
import ImageCropper from './imageCropper';
import ImageDropzone from './imageDropzone';

interface Props {
  loading: boolean;
  uploadImage: (file: Blob) => void;
}

export default function ImageUpload({ loading, uploadImage }: Props) {
  const [file, setFile] = useState<any>([]);
  const [cropper, setCropper] = useState<Cropper>();
  const [open, setOpen] = useState(false);

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
    <Modal
      onClose={() => setOpen(false)}
      onOpen={() => setOpen(true)}
      open={open}
      trigger={<Button type="button">Show Modal</Button>}
    >
      <Modal.Header>Select a Image</Modal.Header>
      <Modal.Content>
        <Grid>
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
          <Grid.Column width={1} />
          <Grid.Column width={4}>
            <Header sub color="teal" content="Step #2 - Add Photo" />
            {file && file.length > 0 && (
              <div
                className="img-preview"
                style={{ minHeight: 200, overflow: 'hidden' }}
              />
            )}
          </Grid.Column>
        </Grid>
      </Modal.Content>
      <Modal.Actions>
        <Button
          loading={loading}
          // eslint-disable-next-line no-sequences
          onClick={() => (onCrop(), setOpen(false))}
          positive
          icon="check"
          type="button"
        />
        <Button
          disabled={loading}
          // eslint-disable-next-line no-sequences
          onClick={() => (setFile([]), setOpen(false))}
          icon="close"
        />
      </Modal.Actions>
    </Modal>
  );
}
