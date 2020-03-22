import { Address } from 'src/app/models/address.model';

export interface PatientSignUp {
  email?: string;
  firstname?: string;
  lastname?: string;
  password?: string;
  confirmPassword?: string;
  address?: Address;
}
