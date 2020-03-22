import { Address } from 'src/app/models/address.model';

export interface MedicalInstituteSignUp {
    email?: string;
    medicalInstituteName?: string;
    password?: string;
    confirmPassword?: string;
    address?: Address;
}