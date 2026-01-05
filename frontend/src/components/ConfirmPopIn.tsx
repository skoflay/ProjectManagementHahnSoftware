// components/ConfirmPopIn.tsx
interface ConfirmPopInProps {
  isOpen: boolean;
  message: string;
  confirmLabel?: string;
  cancelLabel?: string;
  onConfirm: () => void;
  onCancel: () => void;
}

export const ConfirmPopIn = ({
  isOpen,
  message,
  confirmLabel = "Confirm",
  cancelLabel = "Cancel",
  onConfirm,
  onCancel,
}: ConfirmPopInProps) => {
  if (!isOpen) return null;

  return (
    <div className="confirm-popin-overlay">
      <div className="confirm-popin">
        <p>{message}</p>
        <div className="confirm-popin-buttons">
          <button className="btn btn-outline-secondary" onClick={onCancel}>
            {cancelLabel}
          </button>
          <button className="btn btn-danger" onClick={onConfirm}>
            {confirmLabel}
          </button>
        </div>
      </div>
    </div>
  );
};